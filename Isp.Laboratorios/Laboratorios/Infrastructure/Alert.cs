using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Isp.Laboratorios.Infrastructure
{
    public static class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public static void Success(Controller controller, string message, bool dismissable = true)
        {
            AddAlert(controller, "success", message, dismissable, "glyphicon glyphicon-ok-sign");
        }
        public static void Information(Controller controller, string message, bool dismissable = true)
        {
            AddAlert(controller, "info", message, dismissable, "glyphicon glyphicon-info-sign");
        }
        public static void Warning(Controller controller, string message, bool dismissable = true)
        {
            AddAlert(controller, "warning", message, dismissable, "glyphicon glyphicon-warning-sign");
        }
        public static void Danger(Controller controller, string message, bool dismissable = true)
        {
            AddAlert(controller, "danger", message, dismissable, "glyphicon glyphicon-remove-sign");
        }
        public static string GetAllErrorModel(IEnumerable<ModelError> errorList)
        {
            return errorList == null 
                ? string.Empty 
                : errorList.Select(x => x.ErrorMessage).Aggregate((current, next) => current + "<br />" + next);
        }

        private static void AddAlert(ControllerBase controller, string alertType, string message, bool dismissable, string glyphicon)
        {
            var alerts = controller.TempData.ContainsKey(TempDataKey) ? (List<AlertMessage>)controller.TempData[TempDataKey] : new List<AlertMessage>();

            alerts.Add(new AlertMessage
            {
                AlertType = alertType,
                Message = message,
                Dismissable = dismissable,
                Glyphicon = glyphicon
            });

            controller.TempData[TempDataKey] = alerts;
        }

    }
}