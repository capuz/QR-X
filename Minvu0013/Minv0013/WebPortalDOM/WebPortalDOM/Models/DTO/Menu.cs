using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class Menu
    {
        public decimal IdMenu { get; set; }
        public Nullable<decimal> IdMenuPadre { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public Nullable<int> Target { get; set; }
        public Nullable<byte> Activo { get; set; }

    }
}