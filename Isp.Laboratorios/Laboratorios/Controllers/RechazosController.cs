using System.Collections.Generic;
using System.Web.Mvc;
using Isp.Laboratorios.DataAccessLayer;


namespace Isp.Laboratorios.Controllers
{
    public class RechazosController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();

        public ActionResult ObtenerMotivos(List<int> muestrasId)
        {
            _db.ProxyCreationEnabled = false;
            var motivos = _db.MotivosRechazo.ObtenerPorMuestraId(muestrasId);
         
            return Json(motivos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerRechazos(int muestraId, string muestraCodigo)
        {
            ViewBag.MuestraCodigo = muestraCodigo;
            var motivos = _db.Rechazos.ObtenerPorMuestraId(muestraId);
            return PartialView("_ObtenerRechazosModal", motivos);
        }
    }
}