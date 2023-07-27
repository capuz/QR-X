using LibsTest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QrMvc.Controllers
{
    public class ExcelController : Controller
    {
        // GET: ExcelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExcelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExcelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExcelController/Create
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

        // GET: ExcelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExcelController/Edit/5
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

        // GET: ExcelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExcelController/Delete/5
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
        public IActionResult DownloadExcel()
        {
            //var values = new[] {
            //new { Column1 = "MiniExcel", Column2 = 1 },
            //new { Column1 = "Github", Column2 = 2}
            //};
            var memoryStream = ExcelHelper.ExportExcel();
         
       
            //memoryStream.SaveAs(values);
            //memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"{Guid.NewGuid()}.xlsx"
            };
        }
    }
}
