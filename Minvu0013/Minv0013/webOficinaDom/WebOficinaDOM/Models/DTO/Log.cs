using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOficinaDOM.Models.DTO
{
    public class Log
    {
        public int IdLog { get; set; }
        public int? TipoAuditoria { get; set; }
        public string Pagina { get; set; }
        public string Evento { get; set; }
        public string Clase { get; set; }
        public string Cabecera { get; set; }
        public string Mensaje { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public DateTime? Fecha { get; set; }
        public string Usuario { get; set; }
    }
}