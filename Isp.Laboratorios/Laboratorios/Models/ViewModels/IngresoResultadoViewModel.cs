using System.Collections.Generic;

namespace Isp.Laboratorios.Models.ViewModels
{
    public class IngresoResultadoViewModel
    {
        public IngresoResultadoViewModel()
        {
            ResultadosAnalitos = new List<ResultadoAnalitoViewModel>();
            MuestraIdList = new List<int>();
            AnalitoIdList = new List<int>();
            ExamenIdList = new List<int>();
        }
        public List<int> MuestraIdList { get; set; }
        public List<int> ExamenIdList { get; set; }
        public List<int> AnalitoIdList { get; set; }
        public int PrestacionId { get; set; }
        public int MetodoId { get; set; }
        public int NormaId { get; set; }
        public int LimiteCuantificacionId { get; set; }
        public int LimiteDeteccionId { get; set; }
        public int LimiteMaximoPermitidoId { get; set; }
        public int UnidadMedidaId { get; set; }
        public string Resultado { get; set; }
        public string Observacion { get; set; }
        public List<ResultadoAnalitoViewModel> ResultadosAnalitos { get; set; }
    }
}