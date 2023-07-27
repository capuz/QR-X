using System.Web.Optimization;

namespace Isp.Laboratorios
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //script y styles generales
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/ie-emulation-modes-warning.js",
                        "~/Scripts/funcionesAjax.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap.chosen.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/Css/bootstrap-laboratorio.css"));

            //calendario
            bundles.Add(new ScriptBundle("~/bundles/js-datepicker").Include(
            "~/Scripts/bootstrap-datepicker.min.js",
            "~/Scripts/bootstrap-datepicker.es.min.js",
            "~/Scripts/datepicker-config.js"));

            bundles.Add(new StyleBundle("~/bundles/css-datepicker").Include(
            "~/Content/Css/bootstrap-datepicker3.min.css"));

            //dropdown chosen
            bundles.Add(new ScriptBundle("~/bundles/js-chosen").Include(
            "~/Scripts/chosen.jquery.js",
            "~/Scripts/chosen-config.js"));

            bundles.Add(new StyleBundle("~/bundles/css-chosen").Include(
            "~/Content/bootstrap-chosen.css"));

            //grid util
            bundles.Add(new ScriptBundle("~/bundles/js-grid").Include(
            "~/Scripts/checkall-grid.js"));

            //blockUI
            bundles.Add(new ScriptBundle("~/bundles/js-blockUI").Include(
            "~/Scripts/jquery.blockUI.js"));


        }
    }
}
