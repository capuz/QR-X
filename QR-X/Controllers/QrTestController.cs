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
        public IActionResult Form01()
        {
            return View();
        }
        public IActionResult Form02()
        {
            return View();
        }

        // POST: QrTestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string a = collection["PaymentMethod"];
                string m = ""; ;
                //collection.TryGetValue("datos", out m);
                string d= collection["debit"].ToString();
                string c = collection["credit"].ToString();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
