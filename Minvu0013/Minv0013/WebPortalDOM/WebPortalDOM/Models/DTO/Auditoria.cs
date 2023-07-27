using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Models.DTO
{
    public class Auditoria
    {
        public decimal IdAuditoria { get; set; }
        public Nullable<int> TipoTransaccion { get; set; }
        public string Tabla { get; set; }
        public Nullable<decimal> CodigoTablaOrigen { get; set; }
        public string Campo { get; set; }
        public string ValorOriginal { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Usuario { get; set; }

    }
}