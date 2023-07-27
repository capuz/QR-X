
namespace Isp.Laboratorios.Models.ViewModels
{
    public class ExamenViewModel
    {

        public int Id { get; set; }
        public int PrestacionCodigo { get; set; }
        public string PrestacionNombre { get; set; }
        public string LaboratorioNombre { get; set; }
        public int PrestacionDiaMinimo { get; set; }
        public int PrestacionDiaMaximo { get; set; }
        public int PrestacionDias { get; set; }
    }
}