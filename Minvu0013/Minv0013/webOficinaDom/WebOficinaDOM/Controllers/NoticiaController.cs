using Framework.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class NoticiaController : Controller
    {

        private NoticiaBusinessAgent _noticia;
        public ISessionUsuario SessionUsuario { get; set; }


        public NoticiaController()
        {
            _noticia = new NoticiaBusinessAgent();
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
        }
        
        // GET: /Noticia/
        [ValidationActionFilter]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Noticia/List
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult List()
        {
            try
            {
                var noticiaList = _noticia.GetList();
                return PartialView(noticiaList);
            }
            catch (Exception e) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }
            
           
        }

        // GET: Noticia/Create
        [ValidationActionFilter]
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Noticia/Create
        [HttpPost,ValidateInput(false)]
        [ValidationActionFilter]
        public ActionResult Create(Noticia noticia)
            {
            try
            {
                if (ModelState.IsValid)
                {
                    _noticia.PostNoticia(noticia,this.SessionUsuario.Usuario);
                    return RedirectToAction("List");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(noticia);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
        }

        // GET: Noticia/Edit/5
        [ValidationActionFilter]
        public ActionResult Edit(int id)
        {
            try
            {
                var model = _noticia.GetNoticiaById(id);
                return PartialView(model);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
            
        }

        // POST: Noticia/Edit
        [HttpPost, ValidateInput(false)]
        [ValidationActionFilter]
        public ActionResult Edit(Noticia noticia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _noticia.PutNoticia(noticia.IdNoticia,noticia,this.SessionUsuario.Usuario);
                    return RedirectToAction("List");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(noticia);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
        }

        // Preview: 
        [HttpPost, ValidateInput(false)]
        [ValidationActionFilter]
        public ActionResult Preview(Noticia noticia)
        {
            return PartialView(noticia);
        }

       [HttpPost, ValidateInput(false)]
       [ValidationActionFilter]
        public ActionResult BackPreview(Noticia noticia)
        {
            if (noticia.IdNoticia != default(int))
            {
                return PartialView("Edit", noticia);
            }

            return PartialView("Create", noticia);
        }

       // GET: Noticia/DeleteNOTICIA/5
        [ValidationActionFilter]
        public ActionResult Delete(int id)
        {
            try
            {
                _noticia.DeleteNoticia(id, this.SessionUsuario.Usuario);
                return RedirectToAction("List");
            }
            catch (Exception e) 
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }  
        }
        [ValidationActionFilter]
        public ActionResult Publish(int id)
        {
            try
            {
                _noticia.PublishNoticia(id, this.SessionUsuario.Usuario);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
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
                    String path = ConfigWrapper.Value<string>("UploadDir");
                    string fileName = _noticia.UploadFile(files, path);
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