using System.Configuration;
using System.Web;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure
{
    public static class ApplicationInfo
    {
        private static readonly UnitOfWork Db = new UnitOfWork();
        public static string NombreSistema
        {
            get
            {
                return ConfigurationManager.AppSettings["SistemaNombre"];
            }
        }
        private static string Controller
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            }
        }
        private static string Action
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            }
        }
        private static string DbServerName
        {
            get
            {
                return Db.DbContext.Database.Connection.DataSource ?? "No se encuentra DataSource";
            }
        }
        private static string DbDatabaseName
        {
            get
            {
                return Db.DbContext.Database.Connection.Database;
            }
        }
        public static string IP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        public static string Version
        {
            get
            {
                return ConfigurationManager.AppSettings["Version"];
            }
        }
        public static string UrlCambioContrasena
        {
            get
            {
                return ConfigurationManager.AppSettings["MailUrlCambioContrasena"];
            }
        }
        public static string MailSistema
        {
            get
            {
                return ConfigurationManager.AppSettings["MailSistema"];
            }
        }
        public static string Environment
        {
            get
            {
                return Db.Ambientes.ObtenerNombreAmbiente(DbServerName, DbDatabaseName);
            }
        }
        public static Funcionalidad CurrentFunctionality
        {
            get
            {
                return Db.Funcionalidades.ObtenerFuncionalidad(Controller, Action);
            }
        }
        public static readonly string PlantillaCambioContrasena = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PlantillaCambioContrasena"]);
        public static readonly string PlantillaIncidencia = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PlantillaIncidencia"]);
        public static readonly string PlantillaRechazo = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PlantillaRechazo"]);
        public static readonly string RutaHeaderLogo = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ImagenHeaderMail"]);
        public static Usuario CurrentUser
        {
            get
            {
                return Db.Usuarios.ObtenerPorNombre(HttpContext.Current.User.Identity.Name);
            }
        }
    }
}