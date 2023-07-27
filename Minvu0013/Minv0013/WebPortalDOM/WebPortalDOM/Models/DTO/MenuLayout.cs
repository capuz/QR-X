using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class MenuLayout
    {
        public double idMenu { get; set; }
        public double? idMenuPadre { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int Target { get; set; }
        public string TargetString { get; set; }
        public double Nivel { get; set; }
        public string Sort { get; set; }
        public double Hijos { get; set; }
    }
}