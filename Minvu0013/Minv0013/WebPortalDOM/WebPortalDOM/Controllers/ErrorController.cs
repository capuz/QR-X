using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortalDOM.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        [AllowAnonymous]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }
        [AllowAnonymous]
        public ActionResult PaginaEnConstruccion()
        {
            return View();
        }
    }
}