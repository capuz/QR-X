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
    //[RequiresAuthorization]
    public class RecepcionesController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;
        // GET: Recepciones
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult RecepcionSecretaria()
        {
            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.RecepcionSecretaria
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);


            return View(model);
        }
        [HttpPost]
        public ActionResult RecepcionSecretaria(FiltroBusqueda filtroBusqueda)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.RecepcionSecretaria;
            ViewBag.FiltroBusqueda = filtroBusqueda;
            var muestras = _db.Muestras.Obtener(filtroBusqueda);


            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            return View(model);
        }

        public ActionResult RecepcionLaboratorio()
        {
            var filtroBusqueda = new FiltroBusqueda
             {
                 Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                 ExamenEstado = (int)EstadoExamen.RecepcionLaboratorio
             };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);
            return View(model);
        }
        [HttpPost]
        public ActionResult RecepcionLaboratorio(FiltroBusqueda filtroBusqueda)
        {

            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.RecepcionLaboratorio;
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);
            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            return View(model);
        }


        [HttpPost]
        public ActionResult RecepcionarALaboratorio(FiltroBusqueda filtroBusqueda, List<int> muestrasId)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.RecepcionSecretaria;
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var examenes = _db.Examenes.ObtenerPorMuestraLaboratorio(muestrasId, filtroBusqueda.LaboratoriosId);
            foreach (var examen in examenes)
            {
                examen.Observacion = string.Empty;
                examen.EstadoId = (int)EstadoExamen.RecepcionLaboratorio;
            }
            _db.BitacoraExamenes.Insertar(examenes, _usuario.Id);
            _db.GuardarCambios();

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            Alert.Success(this, string.Format("Se han <b>Recepcionado {0} Muestra(s)</b> a Laboratorio", muestrasId.Count));

            return PartialView("_GridRecepcionMuestra", model);
        }
        [HttpPost]
        public ActionResult RecepcionarAIngresoResultados(FiltroBusqueda filtroBusqueda, List<int> muestrasId)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.RecepcionLaboratorio;
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var examenes = _db.Examenes.ObtenerPorMuestraLaboratorio(muestrasId, filtroBusqueda.LaboratoriosId);
            foreach (var examen in examenes)
            {
                examen.Observacion = string.Empty;
                examen.EstadoId = (int)EstadoExamen.IngresoResultados;
            }
            _db.BitacoraExamenes.Insertar(examenes, _usuario.Id);
            _db.GuardarCambios();

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            Alert.Success(this, string.Format("Se han <b>Recepcionado {0} Muestra(s)</b> a Ingreso de Resultados", muestrasId.Count));

            return PartialView("_GridRecepcionMuestra", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RechazarARecepcionMuestras(string muestrasIdChecked, string rechazoComentario, int motivoRechazo, bool enviarMail = false)
        {
            var muestrasId = muestrasIdChecked.Split(',').Select(Int32.Parse).ToList();

            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.RecepcionSecretaria
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var examenes = _db.Examenes.ObtenerPorMuestraLaboratorio(muestrasId, filtroBusqueda.LaboratoriosId);
            rechazoComentario = Sanitizer.GetSafeHtmlFragment(rechazoComentario);//antiXss
            var fechaRechazo = DateTime.Now;
            foreach (var examen in examenes)
            {
                examen.EstadoId = (int)EstadoExamen.RtmDespacharLaboratorio;
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

            Alert.Success(this, string.Format("Se han <b>Rechazado {0} Muestra(s)</b> a RTM", muestrasId.Count));

            return View("RecepcionSecretaria", model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RechazarARecepcionSecretaria(string muestrasIdChecked, string rechazoComentario, int motivoRechazo, bool enviarMail = false)
        {

            var muestrasId = muestrasIdChecked.Split(',').Select(Int32.Parse).ToList();

            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.RecepcionLaboratorio
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var examenes = _db.Examenes.ObtenerPorMuestraLaboratorio(muestrasId, filtroBusqueda.LaboratoriosId);
            rechazoComentario = Sanitizer.GetSafeHtmlFragment(rechazoComentario);//antiXss
            var fechaRechazo = DateTime.Now;
            foreach (var examen in examenes)
            {

                examen.EstadoId = (int)EstadoExamen.RecepcionSecretaria;
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

            Alert.Success(this, string.Format("Se han <b>Rechazado {0} Muestra(s)</b> a Secretaría", muestrasId.Count));
            return View("RecepcionLaboratorio", model);
        }

    }
}