using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure.Security.Authorization
{
    public class RequiresAuthorization : AuthorizeAttribute
    {
        private readonly UnitOfWork _db = new UnitOfWork();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext", "No se a creado un contexto de autorización");

            List<Funcionalidad> funcionalidades = _db.Funcionalidades.ObtenerFuncionalidades(ApplicationInfo.CurrentUser);

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            if (!funcionalidades.Any() || funcionalidades.FirstOrDefault(f => f.Accion == actionName && f.Controlador == controllerName) == null)
                filterContext.Result = filterContext.HttpContext.Request.IsAuthenticated ? new HttpStatusCodeResult(403) : new HttpUnauthorizedResult();

        }
    }
}