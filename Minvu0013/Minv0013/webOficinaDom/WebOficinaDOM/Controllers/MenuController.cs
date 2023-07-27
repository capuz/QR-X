using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Models;
using WebOficinaDOM.Models.DTO;
using System.Net;
using WebOficinaDOM.Session;
using WebOficinaDOM.Filters;
using WebOficinaDOM.Models.Enums;

namespace WebOficinaDOM.Controllers
{
    public class MenuController : Controller
    {
        private MenuBusinessAgent _menu;
        private LogBusinessAgent _log;
        public ISessionUsuario SessionUsuario { get; set; }
        public MenuController()
        {
            HttpSessionStateBase session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
            this.SessionUsuario = SessionFactory.ObtenerUsuario(session);
            _menu = new MenuBusinessAgent();
            _log = new LogBusinessAgent();
        }

        // GET: Menu
        [ValidationActionFilter]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Menu/List
        [ValidationActionFilter]
        public ActionResult List()
        {
            try
            {
                var menuList = _menu.GetMenu();
                return PartialView(menuList);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("Error");
            }

        }

        // GET: /Menu/Edit
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult Edit(int id)
        {
            try
            {
                var menu = _menu.GetMenuById(id);

                if (menu.IdMenuPadre != 1)
                    ViewBag.padres = new SelectList(_menu.GetMenuPadres(), "IdMenu", "Nombre");
                else
                    ViewBag.padres = new SelectList(_menu.GetSinDependencia(), "IdMenu", "Nombre");

                ViewBag.targets = new SelectList(_menu.GetTargets(), "Id", "Nombre"); ;

                return PartialView(menu);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }

        }
        // POST: /Menu/Edit
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult Edit(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _menu.PutMenu(menu, SessionUsuario.Usuario);
                    return RedirectToAction("List");
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                if (menu.IdMenuPadre != 1)
                    ViewBag.padres = new SelectList(_menu.GetMenuPadres(), "IdMenu", "Nombre");
                else
                    ViewBag.padres = new SelectList(_menu.GetSinDependencia(), "IdMenu", "Nombre");

                ViewBag.targets = new SelectList(_menu.GetTargets(), "Id", "Nombre");
                return PartialView(menu);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }
        }

        // GET: /Menu/Create
        [HttpGet]
        [ValidationActionFilter]
        public ActionResult Create()
        {
            try
            {
                ViewBag.padres = new SelectList(_menu.GetMenuPadres(), "IdMenu", "Nombre");

                ViewBag.targets = new SelectList(_menu.GetTargets(), "Id", "Nombre"); ;

                return PartialView();
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ViewBag.error = e.Message;
                return PartialView("ErrorModal");
            }

        }
        // POST: /Menu/Create
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult Create(Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _menu.PostMenu(menu, SessionUsuario.Usuario);
                    return RedirectToAction("List");

                }

                ViewBag.padres = new SelectList(_menu.GetMenuPadres(), "IdMenu", "Nombre");
                ViewBag.targets = new SelectList(_menu.GetTargets(), "Id", "Nombre"); ;

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView(menu);
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
            ViewBag.error = e.Message;
            return PartialView("ErrorModal");
            }
        }
        // POST: /Menu/Delete
        [HttpPost]
        [ValidationActionFilter]
        public ActionResult Delete(int id)
        {
            try
            {
                var menu = _menu.DeleteMenuById(id, SessionUsuario.Usuario);
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                _log.PostLog((int)TipoAuditoria.Error, this.SessionUsuario.Usuario, e.Message, e.StackTrace.ToString());
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return PartialView();
            }
        }
    }
}