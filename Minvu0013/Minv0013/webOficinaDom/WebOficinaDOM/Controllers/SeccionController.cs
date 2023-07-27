using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Models;
using WebOficinaDOM.Models.DTO;
using Framework.Core;
using System.Net;
using System.IO;
using WebOficinaDOM.Session;
using WebOficinaDOM.Filters;
using WebOficinaDOM.Models.Enums;

namespace WebOficinaDOM.Controllers
{
    public class SeccionController : Controller
    {
        private readonly ContenidoPrincipalBusinessAgent _principal;
        private readonly ContenidoSecundarioBusinessAgent _secundario;
        private readonly ContenidoLogoBusinessAgent _logo;
        private LogBusinessAgent _log;
        public ISessionUsuario SessionUsuario { get; set; }
        public SeccionController()
        {
            _principal = new ContenidoPrincipalBusinessAgent();
            _secundario = new ContenidoSecundarioBusinessAgent();
            _logo = new ContenidoLogoBusinessAgent();
            _log = new LogBusinessAgent();
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult LogoDom()
        {
            try
            {
                ViewBag.uriLogo = _logo.GetUriLogoOficina();
                return PartialView();

            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }

        }
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult LogoDom(ContenidoLogo logo)
        {
            try
            {
                ViewBag.uriLogo = _logo.GetUriLogoOficina();
                if (ModelState.IsValid)
                {
                    var ts = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                    var nombreArchivo = ts + logo.File.FileName;
                    string path = ConfigWrapper.Value<string>("UploadDirLogo");

                    var uriLogo = string.Format(ConfigWrapper.Value<string>("UriImagenLogo"),
                                  Path.GetExtension(logo.File.FileName), string.Empty);

                    if (_logo.UploadFile(logo.File, path + nombreArchivo))
                    {
                        _logo.PutLogo(nombreArchivo, SessionUsuario.Usuario);
                        return Json(new { UriLogo = uriLogo });
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return PartialView("LogoDom");
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("LogoDom");
                }

            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("File", e.Message);
                return PartialView("LogoDom");
            }
        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult ListContenidoPrincipal()
        {
            try
            {
                var model = _principal.GetContenido();
                return PartialView(model);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }
        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult CreateContenidoPrincipal()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidationActionFilter]
        public ActionResult CreateContenidoPrincipal(ContenidoPrincipal contenido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contenido.File != null)
                    {
                        var ts = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                        contenido.Icono = ts + contenido.File.FileName;
                        string path = ConfigWrapper.Value<string>("UploadDirContenidoPrincipal") + contenido.Icono;

                        if (_principal.UploadFile(contenido.File, path))
                        {
                            contenido.File = null;
                            _principal.PostContenidoPrincipal(contenido, SessionUsuario.Usuario);
                            return RedirectToAction("ListContenidoPrincipal");
                        }
                    }
                    _principal.PostContenidoPrincipal(contenido, SessionUsuario.Usuario);
                    return RedirectToAction("ListContenidoPrincipal");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(contenido);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("Error", e.Message);
                return PartialView(contenido);
            }
        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult EditContenidoPrincipal(int id)
        {
            try
            {
                var result = _principal.GetContenidoPrincipalById(id);
                return PartialView(result);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }

        }

        [HttpPost]
        [ValidationActionFilter]
        public ActionResult EditContenidoPrincipal(ContenidoPrincipal contenido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contenido.File != null)
                    {
                        var ts = DateTime.Now.ToString("ddMMyyyyHHmmssffff");
                        contenido.Icono = ts + contenido.File.FileName;
                        string path = ConfigWrapper.Value<string>("UploadDirContenidoPrincipal") + contenido.Icono;

                        if (_principal.UploadFile(contenido.File, path))
                        {
                            contenido.File = null;
                            _principal.PutContenidoPrincipal(contenido, SessionUsuario.Usuario);
                            return RedirectToAction("ListContenidoPrincipal");
                        }
                    }
                    _principal.PutContenidoPrincipal(contenido, SessionUsuario.Usuario);
                    return RedirectToAction("ListContenidoPrincipal");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(contenido);

            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("Error", e.Message);
                return PartialView(contenido);
            }
        }
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult DeleteContenidoPrincipal(int id)
        {
            try
            {
                _principal.DeleteContenidoPrincipalById(id, SessionUsuario.Usuario);
                return RedirectToAction("ListContenidoPrincipal");
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { exMessage = e.Message });
            }
        }
        // GET: ContenidoSecundario
        [HttpGet]
        [ValidationActionFilter]
        public PartialViewResult ListContenidoSecundario()
        {
            try
            {
                var model = _secundario.GetContenido();
                return PartialView(model);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }

        }

        [HttpGet]
        [ValidationActionFilter]
        public ActionResult CreateContenidoSecundario()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidationActionFilter]
        public ActionResult CreateContenidoSecundario(ContenidoSecundario contenido)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _secundario.PostContenido(contenido, SessionUsuario.Usuario);
                    return RedirectToAction("ListContenidoSecundario");

                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(contenido);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("Error", e.Message);
                return PartialView(contenido);
            }
        }

        [HttpGet]
        [ValidationActionFilter]
        public PartialViewResult EditContenidoSecundario(int id)
        {
            try
            {
                var result = _secundario.GetContenido(id);
                return PartialView(result);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }

        }

        [HttpPost]
        [ValidationActionFilter]
        public ActionResult EditContenidoSecundario(ContenidoSecundario contenido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _secundario.PutContenido(contenido, SessionUsuario.Usuario);
                    return RedirectToAction("ListContenidoSecundario");

                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(contenido);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ModelState.AddModelError("Error", e.Message);
                return PartialView(contenido);
            }
        }
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult DeleteContenidoSecundario(int id)
        {
            try
            {
                _secundario.DeleteContenidoById(id, SessionUsuario.Usuario);
                return RedirectToAction("ListContenidoSecundario");
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { exMessage = e.Message });
            }
        }
        [HttpGet]
        [ValidationActionFilter]
        public JsonResult GetMaximoContenidoPrincipal()
        {
            var maximo = ConfigWrapper.Value<int>("MaximoContenidoPrincipal");
            return Json(maximo, JsonRequestBehavior.AllowGet);
        }

    }
}