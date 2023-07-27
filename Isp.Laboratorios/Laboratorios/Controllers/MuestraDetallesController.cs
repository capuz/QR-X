using System.Web.Mvc;
using Isp.Laboratorios.DataAccessLayer;

namespace Isp.Laboratorios.Controllers
{
    public class MuestraDetallesController : Controller
    {
        private readonly UnitOfWork _db = new UnitOfWork();
        public ActionResult Obtener(int muestraId)
        {

            var muestra = _db.Muestras.ObtenerDetallePorId(muestraId);
      
            ViewBag.MuestraCodigo = muestra.Codigo;
       
            return PartialView("_ObtenerMuestraDetalleModal", muestra);
        }
    }
}