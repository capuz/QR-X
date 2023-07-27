using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Enums;
using Isp.Laboratorios.Models.ViewModels;

namespace Isp.Laboratorios.Controllers
{
    public class VistosBuenosController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        private readonly Usuario _usuario = ApplicationInfo.CurrentUser;
        // GET: VistosBuenos
        public ActionResult VistoBuenoLaboratorio()
        {
            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.VistoBuenoLaboratorio
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);


            return View(model);
        }
        [HttpPost]
        public ActionResult VistoBuenoLaboratorio(FiltroBusqueda filtroBusqueda)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.VistoBuenoLaboratorio;
            ViewBag.FiltroBusqueda = filtroBusqueda;
            var muestras = _db.Muestras.Obtener(filtroBusqueda);


            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            return View(model);
        }

        public ActionResult VistoBuenoSeccion()
        {
            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.VistoBuenoSeccion
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);


            return View(model);
        }
        [HttpPost]
        public ActionResult VistoBuenoSeccion(FiltroBusqueda filtroBusqueda)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.VistoBuenoSeccion;
            ViewBag.FiltroBusqueda = filtroBusqueda;
            var muestras = _db.Muestras.Obtener(filtroBusqueda);


            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            return View(model);
        }
        public ActionResult VistoBuenoSubdepartamento()
        {
            var filtroBusqueda = new FiltroBusqueda
            {
                Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre"),
                ExamenEstado = (int)EstadoExamen.VistoBuenoSupdepartamento
            };
            ViewBag.FiltroBusqueda = filtroBusqueda;

            var muestras = _db.Muestras.Obtener(filtroBusqueda);

            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);


            return View(model);
        }
        [HttpPost]
        public ActionResult VistoBuenoSubdepartamento(FiltroBusqueda filtroBusqueda)
        {
            filtroBusqueda.Laboratorios = new SelectList(_db.Laboratorios.ObtenerPorUsuarioId(_usuario.Id), "Id", "Nombre");
            filtroBusqueda.ExamenEstado = (int)EstadoExamen.VistoBuenoSupdepartamento;
            ViewBag.FiltroBusqueda = filtroBusqueda;
            var muestras = _db.Muestras.Obtener(filtroBusqueda);


            var model = Mapper.Map<IEnumerable<Muestra>, IEnumerable<MuestraViewModel>>(muestras);

            return View(model);
        }
    }
}