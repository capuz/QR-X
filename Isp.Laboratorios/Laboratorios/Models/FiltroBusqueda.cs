using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Isp.Laboratorios.Models
{
    public class FiltroBusqueda
    {
        public FiltroBusqueda()
        {
            LaboratoriosId=new List<int>();
            Laboratorios = new List<SelectListItem>();
        }

        [Display(Name = "Estado")]
        public int? ExamenEstado { get; set; }
        public IEnumerable<SelectListItem> Laboratorios { get; set; }
        public List<int> LaboratoriosId { get; set; }
        [Display(Name = "Muestra")]
        public string MuestraCodigo { get; set; }
        [Display(Name = "Solicitud")]
        public string SolicitudCodigo { get; set; }
        [Display(Name = "Listado")]
        public int? ListadoNumero { get; set; }
        public DateTime? FechaIngresoDesde { get; set; }
        public DateTime? FechaIngresoHasta { get; set; }
    }
}