
namespace Isp.Laboratorios.Models.ViewModels
{
    public class ResultadoAnalitoViewModel
    {
        public int Id { get; set; }
        public string ExamenMuestraCodigo { get; set; }
        public string ExamenPrestacionCodigo { get; set; }
        public string LimiteDeteccionNombre { get; set; }
        public string LimiteCuantificacionNombre { get; set; }
        public Analito Analito { get; set; }
        public string NormaNombre { get; set; }
        public string LimiteMaximoPermitidoNombre { get; set; }
        public string UnidadMedidaNombre { get; set; }
        public string Resultado { get; set; }
        public string Observacion { get; set; }
    }
}