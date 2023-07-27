using Framework.Core;
using MvcSiteMapProvider.Web.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPortalDOM.Models;
using WebPortalDOM.Session;

namespace WebPortalDOM.Controllers
{
    public class NoticiasController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly NoticiasBusinessAgent _noticias;
        public ISessionUsuario SessionUsuario { get; set; }
        public NoticiasController() {
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
            _noticias = new NoticiasBusinessAgent();

        }
        // GET: /Noticias/Index
        public ActionResult Index()
        {
            try
            {
                GlobalDiagnosticsContext.Set("TipoAuditoria", 1);
                logger.Info("Ingreso a Noticias");
                var noticias = _noticias.GetNoticiasDestacadas();
                return View(noticias);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }
    
        }
        // GET: /Noticias/Bajadas
        public ActionResult Bajadas()
        {
            try
            {
                var noticias = _noticias.GetNoticiasDestacadas();
                return PartialView(noticias);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }

        }
        // GET: /Noticias/Historicas
        public ActionResult Historicas()
        {
            try
            {
                GlobalDiagnosticsContext.Set("TipoAuditoria", 1);
                logger.Info("Ingreso a Noticias Historicas");
                var noticias = _noticias.GetNoticiasHistoricas();

                return PartialView(noticias);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }

        }
        // GET: /Noticias/Detalle
        [SiteMapTitle("Titular")] 
        public ActionResult Detalle(int? id)
        {
            try
            {
                GlobalDiagnosticsContext.Set("TipoAuditoria", 1);
                logger.Info("Ingreso a Noticias Detalle");
                var noticias = _noticias.GetNoticia(id);
                return View(noticias);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }
        }
	}
}