using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Isp.Laboratorios.Models.FiltrosEstadisticas
{
    public class FiltroEstadistica
    {

        public FiltroEstadistica()
        {
            LaboratoriosId=new List<int>();
            Laboratorios = new List<SelectListItem>();
        }

        [Display(Name = "Código Laboratorio")]
        public int LaboratorioId { get; set; }
        [Display(Name = "Solicitud")]
        public string CodigoSolicitud { get; set; }
        [Display(Name = "Número Formulario")]
        public int NumeroFormulario { get; set; }
        [Display(Name = "Muestra")]
        public string CodigoMuestra { get; set; }
        [Display(Name = "Laboratorio")]
        public string NombreLaboratorio { get; set; }

        [Display(Name = "Estado")]
        public int? ExamenEstado { get; set; }
        [Display(Name = "Código Prestacion")]
        public int? PrestacionId { get; set; }

        public IEnumerable<SelectListItem> Laboratorios { get; set; }
        public List<int> LaboratoriosId { get; set; }

        [Display(Name = "Listado")]
        public int? ListadoNumero { get; set; }
        public DateTime? FechaIngresoDesde { get; set; }
        public DateTime? FechaIngresoHasta { get; set; }

    }
}