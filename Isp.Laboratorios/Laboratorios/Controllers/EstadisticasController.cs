using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using AutoMapper;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models.Enums;
using Isp.Laboratorios.Models.ViewModels;
using Isp.Laboratorios.Models.FiltrosEstadisticas;

namespace Isp.Laboratorios.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;
        // GET: Busquedas

        public ActionResult EstadisticaResultadosMasivos()
        {
            ViewBag.LaboratoriosId = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            ViewBag.ExamenEstado = new SelectList(_db.Estados.ObtenerTodo(), "Id", "Nombre");
            ViewBag.PrestacionId = new SelectList(_db.Prestaciones.ObtenerTodo(), "Id", "Nombre");

            return View(new List<Examen>());
        }


        [HttpPost]
        public ActionResult EstadisticaResultadosMasivos(FiltroEstadistica filtroEstadistica)
        {
            ViewBag.LaboratoriosId = new SelectList(_db.Laboratorios.ObtenerTodo(), "Id", "Nombre");
            ViewBag.ExamenEstado = new SelectList(_db.Estados.ObtenerTodo(), "Id", "Nombre");
            ViewBag.PrestacionId = new SelectList(_db.Prestaciones.ObtenerTodo(), "Id", "Nombre");

            List<Examen> examenes = _db.Examenes.Obtener(filtroEstadistica);
            return View(examenes);
        }
    }
}