using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class Noticia
    {
        public decimal IdNoticia { get; set; }
        public string Titular { get; set; }
        public string Bajada { get; set; }
        public string Cuerpo { get; set; }
        public string Imagen { get; set; }
        public Nullable<byte> Destacada { get; set; }
        public Nullable<decimal> Orden { get; set; }
        public Nullable<System.DateTime> FechaPublicacion { get; set; }
        public Nullable<byte> Activo { get; set; }

    }
}