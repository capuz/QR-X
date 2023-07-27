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
        const string ENVIRONMENT_MINVU = "minvu";


        public static bool EnvironmentIsMinvu
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"]
                    .Equals(WebConfig.ENVIRONMENT_MINVU, StringComparison.OrdinalIgnoreCase);
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
    }
}