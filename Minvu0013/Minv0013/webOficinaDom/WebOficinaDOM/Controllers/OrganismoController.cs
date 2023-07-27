using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Filters;
using WebOficinaDOM.Models;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Session;

namespace WebOficinaDOM.Controllers
{
    public class OrganismoController : Controller
    {
        private OrganismoBusinessAgent _organismo;
        public ISessionUsuario SessionUsuario { get; set; }

        public OrganismoController()
        {
            _organismo = new OrganismoBusinessAgent();
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
        }

        // GET: Organismo
        [ValidationActionFilter]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Organismo/List
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult List()
        {
            try
            {
                var noticiaList = _organismo.GetList();
                return PartialView(noticiaList);
            }
            catch(Exception e) {

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }
            
        }

        // GET: Organismo/Create
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult Create(Organismo organismo)
        {
            ModelState.Clear();
            try
            {
                organismo.OrganismoTipoList = _organismo.GetOrganismoTipoList();
                organismo.OrganismoPadreList = _organismo.GetOrganismoPadreList();
                return PartialView(organismo);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
           
        }

        // POST: Organismo/Create
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult CreateNew(Organismo organismo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _organismo.PostOrganismo(this.SessionUsuario.Usuario,organismo);
                    return RedirectToAction("List");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                organismo.OrganismoTipoList = _organismo.GetOrganismoTipoList();
                organismo.OrganismoPadreList = _organismo.GetOrganismoPadreList();
                return PartialView("Create", organismo);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
        }

        // GET: Organismo/Edit/5
        [ValidationActionFilter]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = _organismo.GetOrganismoById(id);
                model.OrganismoTipoList = _organismo.GetOrganismoTipoList();
                model.OrganismoPadreList = _organismo.GetOrganismoPadreList();
                return PartialView(model);
            }
            catch (Exception e) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
           
        }

        // POST: Organismo/Edit
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult Edit(Organismo organismo, string text)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _organismo.PutOrganismo(organismo.IdOrganismo,this.SessionUsuario.Usuario, organismo);
                    return RedirectToAction("Search", new {text=text });
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                organismo.OrganismoTipoList = _organismo.GetOrganismoTipoList();
                organismo.OrganismoPadreList = _organismo.GetOrganismoPadreList();
                return PartialView(organismo);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
        }

        // GET: Noticia/DeleteORGANISMO/5
        [ValidationActionFilter]
        public ActionResult Delete(int id, string text)
        {
            try
            {
                _organismo.DeleteOrganismo(id, this.SessionUsuario.Usuario);
                return RedirectToAction("Search", new {text=text });
            }
            catch (Exception e) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
            
        }

        // GET: /Organismo/List
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult Search(String text)
        {
            try
            {
                var noticiaList = _organismo.GetListSearch(text);
                return PartialView("List", noticiaList);
            }
            catch  (Exception e) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
            
        }

        [ValidationActionFilter]
        public JsonResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    String path = ConfigWrapper.Value<string>("UploadDirOrganismo");
                    string fileName = _organismo.UploadFile(files, path);
                    var result = new
                    {
                        fname = fileName
                    };
                    return Json(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var result = new
                {
                    fname = ""
                };

                return Json(result);
            }

        }
    }
}