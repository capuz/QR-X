using System;
using System.Collections.Generic;

namespace Isp.Laboratorios.Models.ViewModels
{
    public class MuestraViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string ProcedenciaNombre { get; set; }
        public DateTime SolicitudFecha { get; set; }
        public string TipoMuestraNombre { get; set; }
        public IEnumerable<ExamenViewModel> Examenes { get; set; }
    }
}