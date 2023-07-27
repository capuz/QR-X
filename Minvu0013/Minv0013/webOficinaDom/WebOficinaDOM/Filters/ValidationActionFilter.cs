using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Session;

namespace WebOficinaDOM.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        enum Status
        {
            OK,
            NoSession,
            Forbidden,
            OffSite
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            Status status = Status.OK;

            string pageRedirect;

            if (status == Status.OK)
            {
                status = this.ValidaUsuario(filterContext);
            }

            if (status == Status.NoSession)
            {
                pageRedirect = WebConfig.PageLogin;
                //filterContext.Result = new RedirectResult(pageRedirect, false);

                filterContext.Result = new PartialViewResult
                {
                    ViewName = WebConfig.PageLogin
                };

                return;
            }

            if (filterContext.Controller.ViewData.ModelState.IsValid) return;
        }



        private Status ValidaUsuario(ActionExecutingContext filterContex)
        {
            HttpRequestBase request = filterContex.HttpContext.Request;
            ISessionUsuario usuario = SessionFactory.ObtenerUsuario(filterContex.HttpContext.Session);
            Status result = Status.NoSession;
            if (usuario.IsNotNull())
            {
                if (usuario.Rut != default(int))
                {
                    result = Status.OK;
                }
                else if (!WebConfig.EnviromentIsMinvu)
                {
                    usuario = SessionFactory.AsignarUsuario(filterContex.HttpContext.Session, WebConfig.RutDummy,WebConfig.UserDummy);
                    result = Status.OK;
                }
            }

            return result;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ///TODO: Hook posterior a la ejecucion de la accion
        }
    }
}