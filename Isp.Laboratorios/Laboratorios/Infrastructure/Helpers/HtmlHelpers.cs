using System;
using System.Web.Mvc;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure.Helpers
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString Breadcrumbs(this HtmlHelper helper)
        {
            Funcionalidad funcionalidad = ApplicationInfo.CurrentFunctionality;

            var div = new TagBuilder("div");
            div.AddCssClass("h6");

            var olBreadcrumb = new TagBuilder("ol");
            olBreadcrumb.AddCssClass("breadcrumb col-md-7");

            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "/Home/Index");
            a.SetInnerText("Home");
            li.InnerHtml += a.ToString();
            olBreadcrumb.InnerHtml += li.ToString();

            if (funcionalidad != null)
                CreateBreadcrumbs(ref olBreadcrumb, funcionalidad);

            var olAppInformation = new TagBuilder("ol");
            olAppInformation.AddCssClass("breadcrumb text-right col-md-5");

            var liEnvironment = new TagBuilder("li");
            liEnvironment.SetInnerText(String.Format("Ambiente : {0}", ApplicationInfo.Environment));

            var liRol = new TagBuilder("li");
            liRol.SetInnerText(String.Format("Perfil : {0}", ApplicationInfo.CurrentUser.Rol.Nombre));

            var liIP = new TagBuilder("li");
            liIP.SetInnerText(String.Format("IP : {0}", ApplicationInfo.IP));

            var liVersion = new TagBuilder("li");
            liVersion.SetInnerText(String.Format("Versión : {0}", ApplicationInfo.Version));

            olAppInformation.InnerHtml += liEnvironment.ToString();
            olAppInformation.InnerHtml += liRol.ToString();
            olAppInformation.InnerHtml += liIP.ToString();
            olAppInformation.InnerHtml += liVersion.ToString();

            div.InnerHtml += olBreadcrumb.ToString();
            div.InnerHtml += olAppInformation.ToString();

            return MvcHtmlString.Create(div.ToString());
        }

        private static void CreateBreadcrumbs(ref TagBuilder ol, Funcionalidad funcionalidad)
        {
            if (funcionalidad.FuncionalidadPadre != null && !funcionalidad.FuncionalidadPadre.Titulo.Equals("[NOMBRE_USUARIO]"))
            {
                CreateBreadcrumbs(ref ol, funcionalidad.FuncionalidadPadre);
            }

            var li = new TagBuilder("li");

            var a = new TagBuilder("a");
            a.MergeAttribute("href", string.Format("/{0}/{1}", funcionalidad.Controlador, funcionalidad.Accion));
            a.SetInnerText(funcionalidad.Titulo);

            li.InnerHtml += a.ToString();
            ol.InnerHtml += li.ToString();

        }
    }
}