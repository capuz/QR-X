using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebOficinaDOM
{
    public class WebConfig
    {
        const string TRUE = "true";
        const string ENVIROMENT_MINVU = "minvu";

        public static bool EnviromentIsMinvu
        {
            get
            {
                return ConfigurationManager.AppSettings["Enviroment"]
                    .Equals(WebConfig.ENVIROMENT_MINVU, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static int RutDummy
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["RutDummy"]);
            }
        }

        public static string UserDummy
        {
            get
            {
                return ConfigurationManager.AppSettings["UserDummy"];
            }
        }

        public static string PageLogin
        {
            get { return ConfigurationManager.AppSettings["PageLogin"]; }
        }
    }
}