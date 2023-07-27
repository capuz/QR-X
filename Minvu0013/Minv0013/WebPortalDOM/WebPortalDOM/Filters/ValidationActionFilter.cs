using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM;
using WebPortalDOM.Session;

namespace WebPortalDOM.Filters
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

            if (status == Status.OK)
            {
                status = this.ValidaUsuario(filterContext);
            }

            if (filterContext.Controller.ViewData.ModelState.IsValid) return;
        }



        private Status ValidaUsuario(ActionExecutingContext filterContex)
        {
            HttpRequestBase request = filterContex.HttpContext.Request;
            ISessionUsuario usuario = SessionFactory.ObtenerUsuario(filterContex.HttpContext.Session);
            Status result = Status.NoSession;
            if (usuario!=null)
            {
                if (usuario.Rut != default(int))
                {
                    result = Status.OK;
                }
                else if (!WebConfig.EnvironmentIsMinvu)
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