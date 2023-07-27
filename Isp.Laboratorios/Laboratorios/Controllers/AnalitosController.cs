using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Enums;
using Isp.Laboratorios.Models.ViewModels;

namespace Isp.Laboratorios.Controllers
{
    public class AnalitosController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        // GET: Analitos
        public ActionResult ObtenerPorMetodoPrestacion(int? prestacionId, int? metodoId)
        {
            _db.ProxyCreationEnabled = false;

            var analitos = prestacionId != null
                ? _db.Analitos.ObtenerPorPrestacionId((int)prestacionId)
                : null;

            return Json(analitos, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///  ingresoResultados #btnAgregarAnalito
        /// </summary>
        /// <param name="ingresoResultado"></param>
        /// <returns></returns>
        public ActionResult AgregarResultadoAnalito(IngresoResultadoViewModel ingresoResultado)
        {

            var resultadosAnalitos = new List<ResultadoAnalito>();

            foreach (var examenId in ingresoResultado.ExamenIdList)
            {
                var ex = _db.Examenes.ObtenerPorId(examenId);
                //analito
                var analitoIdList = ex.Prestacion.AnalitosPorPrestaciones.Where(x => ingresoResultado.AnalitoIdList.Contains(x.AnalitoId)).Select(x => x.AnalitoId);

                foreach (var analitoId in analitoIdList)
                {
                    var resultadoAnalito = new ResultadoAnalito
                    {
                        ExamenId = examenId,
                        AnalitoId = analitoId,
                        LimiteCuantificacionId = ingresoResultado.LimiteCuantificacionId,
                        LimiteDeteccionId = ingresoResultado.LimiteDeteccionId,
                        LimiteMaximoPermitidoId = ingresoResultado.LimiteMaximoPermitidoId,
                        NormaId = ingresoResultado.NormaId,
                        UnidadMedidaId = ingresoResultado.UnidadMedidaId,
                        Resultado = ingresoResultado.Resultado,
                        Observacion = ingresoResultado.Observacion,
                        MetodoId = ingresoResultado.MetodoId,
                        Fecha = DateTime.Now,
                        Informar = true
                    };
                    resultadosAnalitos.Add(resultadoAnalito);
                }
            }

            _db.ResultadosAnalitos.Insertar(resultadosAnalitos);

            var bitacoraResAnalito = Mapper.Map<List<ResultadoAnalito>, List<BitacoraResultadoAnalito>>(resultadosAnalitos);

            _db.BitacoraResultadosAnalitos.Insertar(bitacoraResAnalito, ApplicationInfo.CurrentUser.Id);

            try
            {
                _db.GuardarCambios();
            }
            catch (Exception ex)
            {
                ex = ex;
            }

            List<ResultadoAnalitoViewModel> model;
            using (var db = new UnitOfWork())
            {
                resultadosAnalitos = db.ResultadosAnalitos.ObtenerPorExamenIds(ingresoResultado.ExamenIdList);
                model = Mapper.Map<List<ResultadoAnalito>, List<ResultadoAnalitoViewModel>>(resultadosAnalitos);
            }

            return PartialView("_GridIngresoResultados", model);
        }
        public ActionResult EliminarResultadoAnalito(int analitoId)
        {
            _db.ResultadosAnalitos.EliminarPorId(analitoId);

            return PartialView("_GridIngresoResultados");
        }
    }
}