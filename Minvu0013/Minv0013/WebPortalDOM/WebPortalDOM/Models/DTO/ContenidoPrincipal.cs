using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class ContenidoPrincipal
    {
        public decimal IdContenidoPrincipal { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public Nullable<byte> Activo { get; set; }
        public int NumCol { get; set; }
        public int NumColXs { get; set; }
    }
}