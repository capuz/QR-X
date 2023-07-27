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


namespace Isp.Laboratorios.Controllers
{
    public class OtrosController : Controller
    
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;
        // GET: Busquedas

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerBitacora()
        {
            ViewBag.LaboratoriosId = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            var listaAntiguedad = new List<KeyValuePair<int, string>>();
            listaAntiguedad.Add(new KeyValuePair<int, string>(1, "1 Semana"));
            listaAntiguedad.Add(new KeyValuePair<int, string>(2, "2 Semanas"));
            ViewBag.Antiguedad = new SelectList(listaAntiguedad, "Key", "Value");
            //ViewBag.Tipo = new SelectList(_db.)
            return View(new List<Examen>());
        }
        [HttpPost]
        public ActionResult ObtenerBitacora(FiltroBusquedaGeneral filtroBusquedaGeneral)
        {
            ViewBag.LaboratoriosId = new SelectList(_db.Laboratorios.ObtenerTodo(), "Id", "Nombre");
            var listaAntiguedad = new List<KeyValuePair<int, string>>();
            listaAntiguedad.Add(new KeyValuePair<int, string>(1, "1 Semana"));
            listaAntiguedad.Add(new KeyValuePair<int, string>(2, "2 Semanas"));
            ViewBag.Antiguedad = new SelectList(listaAntiguedad, "Key", "Value");
            

            List<Examen> examenes = _db.Examenes.Obtener(filtroBusquedaGeneral);
            return View(examenes);
        }

        public ActionResult NuevaObservacion() {
            ViewBag.LaboratoriosId = new SelectList(_db.Laboratorios.ObtenerTodo(), "Id", "Nombre");
           
            return View();
        }

	}
}