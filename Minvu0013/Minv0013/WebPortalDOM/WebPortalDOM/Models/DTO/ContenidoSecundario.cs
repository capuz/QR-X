using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class ContenidoSecundario
    {
        public decimal IdContenidoSecundario { get; set; }
        public string Url { get; set; }
        public Nullable<byte> Activo { get; set; }
    }
}