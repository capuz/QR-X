using System.ComponentModel.DataAnnotations;

namespace Isp.Laboratorios.Models.Authentication
{
    public class CambioContrasena
    {
        [Required(ErrorMessage = "Ingrese contraseña actual")]
        [DataType(DataType.Password)]
        public string ContrasenaActual { get; set; }

        [Required(ErrorMessage = "Ingrese nueva contraseña")]
        [StringLength(40, ErrorMessage = "la Contraseña tiene que tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string ContrasenaNueva { get; set; }

        [Required(ErrorMessage = "Ingrese nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("ContrasenaNueva", ErrorMessage = "Las contraseñas deben ser iguales")]
        public string ContrasenaNuevaAConfirmar { get; set; }            
    }
}