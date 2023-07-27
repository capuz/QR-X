using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Session;
using WebOficinaDOM.Models;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Models.Enums;
using WebOficinaDOM.Filters;
namespace WebOficinaDOM.Controllers
{
    public class HomeController : Controller
    {
        private LogBusinessAgent _log;
        public ISessionUsuario SessionUsuario { get; set; }

        public HomeController()
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
            _log = new LogBusinessAgent();
        }
        [ValidationActionFilter]
        public ActionResult Index()
        {

            _log.PostLog((int)TipoAuditoria.Navegacion, this.SessionUsuario.Usuario, "Ingreso a Oficina", "Ingreso a Oficina");

            return View();
        }

    }
}