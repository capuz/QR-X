using System.ComponentModel.DataAnnotations;

namespace Isp.Laboratorios.Models.Authentication
{
    public class Login
    {
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "Ingrese nombre de usuario")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Ingrese contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}