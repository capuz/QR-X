using System.Web.Mvc;

namespace Isp.Laboratorios.Controllers
{
    public class AlertsController : Controller
    {
        public ActionResult Render()
        {
            return PartialView("_Alerts");
        }
    }
}