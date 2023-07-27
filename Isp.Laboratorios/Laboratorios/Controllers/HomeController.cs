using System.Web.Mvc;
using Isp.Laboratorios.Infrastructure.Security.Authorization;

namespace Isp.Laboratorios.Controllers
{
    [RequiresAuthorization]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}