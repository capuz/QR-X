using Framework.Core;
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
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly MenuBusinessAgent _menu;
        private readonly ContenidoPrincipalBusinessAgent _principal;
        private readonly ContenidoSecundarioBusinessAgent _secundario;
        private readonly ContenidoLogoBusinessAgent _logo;
        public ISessionUsuario SessionUsuario { get; set; }
        public HomeController()
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
            _menu = new MenuBusinessAgent();
            _principal = new ContenidoPrincipalBusinessAgent();
            _secundario = new ContenidoSecundarioBusinessAgent();
            _logo = new ContenidoLogoBusinessAgent();

        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            try
            {
                GlobalDiagnosticsContext.Set("TipoAuditoria", 1);
                logger.Info("Ingreso a Homepage Index");
          

                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }
            
        }
        // GET: /Home/Logo
        public ActionResult Logo()
        {
            try
            {
                ViewBag.uriLogo = _logo.GetUriLogo();
                return PartialView();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }

        }
        // GET: /Home/ContenidoPrincipal
        public ActionResult ContenidoPrincipal()
        {
            try
            {
                var contenido = _principal.GetContenidoActivo();

                return PartialView(contenido);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }

        }
        // GET: /Home/ContenidoSecundario
        public ActionResult ContenidoSecundario()
        {
            try
            {
                var contenido = _secundario.GetContenidoActivo();
                return PartialView(contenido);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = ex.Message;
                return PartialView("Error");
            }

        }
        // GET: /Home/Menu
        public ActionResult Menu()
        {
            try
            {
                var menu = _menu.GetMenuLayout();
                return PartialView(menu);
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