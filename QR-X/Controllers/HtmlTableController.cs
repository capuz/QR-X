using HtmlTableHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QrMvc.Controllers
{
    public class HtmlTableController : Controller
    {
        public class Prop
        {
            public string border { set; get; }
        }
        // GET: HtmlTableController
        public ActionResult Index()
        {
            var data = new[] { new { Name = "ITWeiHan", Age = "25", Gender = "M" },
                    new { Name = "Francesco", Age = "37", Gender = "M" },
                    new { Name = "Amalia", Age = "7", Gender = "F" }};
            ViewBag.Table = data.ToHtmlTable(new Prop { border = "1" });
            return View();
        }

        // GET: HtmlTableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HtmlTableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HtmlTableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HtmlTableController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HtmlTableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HtmlTableController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HtmlTableController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
