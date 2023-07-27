using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Enums;
using Isp.Laboratorios.Models.ViewModels;
using Microsoft.Security.Application;

namespace Isp.Laboratorios.Controllers
{
    public class ResultadosController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;

        public ActionResult ParaIngresoResultados()
        {
            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.IngresoResultados
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);
            return View(model);
        }

        public ActionResult IngresoResultados(List<int> examenesId)
        {
            var examenes = _db.Examenes.ObtenerPorIds(examenesId);
            var muestras = examenes.Select(x => x.Muestra).ToList();

            ViewBag.lblMuestraCodigo = string.Join(", ", muestras.Select(x => x.Codigo).Distinct());

            ViewBag.TipoMuestraNombre = string.Join(", ", muestras.Select(x => x.TipoMuestra.Nombre).Distinct());

            var prestaciones = examenes.Select(x => x.Prestacion).ToList();
            ViewBag.ddlPrestaciones = new SelectList(prestaciones, "Id", "Nombre");

            var analitos = _db.Analitos.ObtenerPorPrestaciones(prestaciones);

            ViewBag.MuestraIdList = muestras.Select(x => x.Id).ToList();
            ViewBag.ExamenIdList = examenesId;
            ViewBag.AnalitoIdList = analitos.Select(x => x.Id).ToList();

            ViewBag.ddlMetodos = new SelectList(_db.Metodos.ObtenerPorAnalitos(analitos), "Id", "Nombre");

            ViewBag.ddlNormas = new SelectList(_db.Normas.ObtenerTodo(), "Id", "Nombre");

            ViewBag.ddlLimitesCuantificaciones = new SelectList(_db.LimitesCuentificaciones.ObtenerTodo(), "Id", "Nombre");

            ViewBag.ddlLimitesDetecciones = new SelectList(_db.LimitesDetecciones.ObtenerTodo(), "Id", "Nombre");

            ViewBag.ddlLimitesMaximosPermitidos = new SelectList(_db.LimitesMaximosPermitidos.ObtenerTodo(), "Id", "Nombre");

            ViewBag.ddlUnidadesMedida = new SelectList(_db.UnidadesMedidas.ObtenerTodo(), "Id", "Nombre");


            var ra = _db.ResultadosAnalitos.ObtenerPorExamenIds(ViewBag.ExamenIdList as List<int>);
            var model = new IngresoResultadoViewModel
            {
                ResultadosAnalitos =  Mapper.Map<List<ResultadoAnalito>, List<ResultadoAnalitoViewModel>>(ra)
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("IngresoResultadosMasivo",model);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RechazarARecepcionLaboratorio(string muestrasIdChecked, string rechazoComentario, int motivoRechazo, bool enviarMail = false)
        {
            var muestrasId = muestrasIdChecked.Split(',').Select(Int32.Parse).ToList();

            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.IngresoResultados
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var examenes = _db.Examenes.ObtenerPorMuestraLaboratorio(muestrasId, filtroBusqueda.LaboratoriosId);
            rechazoComentario = Sanitizer.GetSafeHtmlFragment(rechazoComentario);//antiXss
            var fechaRechazo = DateTime.Now;
            foreach (var examen in examenes)
            {

                examen.EstadoId = (int)EstadoExamen.RecepcionLaboratorio;
                examen.Observacion = rechazoComentario;

                var rechazo = new Rechazo
                {
                    UsuarioId = _usuario.Id,
                    ExamenId = examen.Id,
                    Fecha = fechaRechazo,
                    MotivoRechazoId = motivoRechazo,
                    Observacion = rechazoComentario
                };

                _db.Rechazos.Insertar(rechazo);
            }
            _db.BitacoraExamenes.Insertar(examenes, _usuario.Id);

            if (enviarMail)
            {
                //envio de mail por muestra
                foreach (var muestraId in muestrasId)
                {
                    var muestra = _db.Muestras.ObtenerPorId(muestraId);
                    var laboratorio = _db.Laboratorios.ObtenerPorMuestraId(muestraId);
                    var responsableRtm = _db.Usuarios.ObtenerResponsableRtm();

                    string ruta = ApplicationInfo.PlantillaRechazo;
                    System.IO.File.ReadAllText(ruta, Encoding.UTF8);
                    string htmlString = System.IO.File.ReadAllText(ruta, Encoding.UTF8);

                    var tokens = new Dictionary<string, string>
                                                {
                                                    {"[SistemaNombre]",ApplicationInfo.NombreSistema},
                                                    {"[UsuarioOrigen]",_usuario.Nombre + " " +_usuario.ApellidoPaterno},
                                                    {"[UsuarioDestino]",responsableRtm.Nombre + " "+responsableRtm.ApellidoPaterno},
                                                    {"[MuestraCodigo]",muestra.Codigo},
                                                    {"[LaboratorioNombre]",laboratorio.Nombre},
                                                    {"[ProcedenciaNombre]",muestra.Solicitud.Procedencia.Nombre},
                                                    {"[Comentario]",rechazoComentario}
                       
                                                };

                    var mensaje = Util.ReplaceTokens(htmlString, tokens);

                    string asunto = "Notificación de Rechazos de la Muestra :" + muestra.Codigo + " " + ApplicationInfo.NombreSistema;

                    var mail = Mail.Generar(ApplicationInfo.MailSistema, asunto, responsableRtm.CorreoElectronico, mensaje, laboratorio.Seccion.ResponsableMail, MailPriority.High);

                    Mail.Enviar(mail);
                }
            }

            _db.GuardarCambios();

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            Alert.Success(this, string.Format("Se han <b>Rechazado {0} Muestra(s)</b> a Laboratorio", muestrasId.Count));
            return View("ParaIngresoResultados", model);
        }

    }
}