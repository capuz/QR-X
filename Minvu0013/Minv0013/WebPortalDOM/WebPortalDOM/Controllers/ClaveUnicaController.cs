using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Core;
using WebPortalDOM.Session;

namespace WebPortalDOM.Controllers
{
    public class ClaveUnicaController : Controller
    {
        public ISessionUsuario SessionUsuario { get; set; }
        public ClaveUnicaController()
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
        }
        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return PartialView();
        }

        public RedirectResult ObtenerUrl()
        {

            ErrorController Error = new ErrorController();

            var idSitio = ConfigWrapper.Value<string>("Id_Sitio");
            var Usuario = ConfigWrapper.Value<string>("Usuario_Sitio");
            var Clave = ConfigWrapper.Value<string>("Clave_Sitio");

            wsClaveUnicaMinvu.WSClaveUnica wsCU = new wsClaveUnicaMinvu.WSClaveUnica();
            wsClaveUnicaMinvu.RequestTypeURL Entrada = new
            wsClaveUnicaMinvu.RequestTypeURL();
            wsClaveUnicaMinvu.ResponseTypeURL Respuesta = new
            wsClaveUnicaMinvu.ResponseTypeURL();
            Entrada.IdSitio = Convert.ToInt32(idSitio);
            Entrada.Usuario = Usuario;
            Entrada.Clave = Clave;
            Respuesta = wsCU.ObtenerURL(Entrada);
            Session["State"] = Respuesta.State;

            if (Respuesta.URL != null)
            {
                return Redirect(Respuesta.URL.ToString());
            }
            else
            {
                return Redirect("/error/index");
            }
        }


        public void ObtenerCredenciales(string respuesta)
        {
            /*Inicio acceso ClaveUnica*/
            if (respuesta == "OK")
            {
                wsClaveUnicaMinvu.WSClaveUnica wsCU = new
                wsClaveUnicaMinvu.WSClaveUnica();
                wsClaveUnicaMinvu.RequestTypeCredencial Entrada = new
                wsClaveUnicaMinvu.RequestTypeCredencial();
                wsClaveUnicaMinvu.ResponseTypeCredencial Respuesta = new
                wsClaveUnicaMinvu.ResponseTypeCredencial();
                Entrada.IdSitio = Convert.ToInt32(ConfigWrapper.Value<string>("Id_Sitio"));
                Entrada.State = Session["State"].ToString();
                Respuesta = wsCU.ObtenerCredenciales(Entrada);
                Redirect("/Home/Index");
            }
            else if (respuesta == "NOK")
            {
                Redirect("/Error/Index");
            }

        }

    }
}