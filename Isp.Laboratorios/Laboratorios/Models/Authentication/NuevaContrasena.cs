using System.ComponentModel.DataAnnotations;

namespace Isp.Laboratorios.Models.Authentication
{
    public class NuevaContrasena
    {
        [Required(ErrorMessage = "El enlace es incorrecto")]
        public string DataEnlace { get; set; }

        [Required(ErrorMessage = "Ingrese Correo")]
        [Compare("DataEnlace", ErrorMessage = "Correo incorrecto para recuperación de contraseña")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Ingrese nueva contraseña")]
        [StringLength(40, ErrorMessage = "la Contraseña tiene que tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        public string ContrasenaNueva { get; set; }

        [Required(ErrorMessage = "Reingrese nueva contraseña")]
        [Compare("ContrasenaNueva", ErrorMessage = "Las contraseñas deben ser iguales")]
        public string ContrasenaNuevaAConfirmar { get; set; }

    }
}