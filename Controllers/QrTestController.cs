using Microsoft.AspNetCore.Mvc;

namespace QrMvc.Controllers
{
    public class QrTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestAdminLTE()
        {
            return View();
        }

    }
}
