using GridMvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    public class ContenidoPrincipal
    {
        public int IdContenidoPrincipal { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "Requerido")]
        [StringLength(75)]
        [RegularExpression("^[\\dA-zñÑá-úÁ-Ú.,_\\-\\s]+$",
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "CaracteresNoPermitidos")]
        public string Nombre { get; set; }

        [Display(Name = "Icono")]
        [WebOficinaDOM.ValidationAttributes.MaxFileSize(3 * 1024 * 1024, ErrorMessage = "El archivo no debe sobrepasar los {0} MB")]
        [WebOficinaDOM.ValidationAttributes.FileExtensions("jpg,jpeg,png,gif", ErrorMessage = "Seleccione archivo de tipo .png .jpg o .gif")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(2048)]
        public string Descripcion { get; set; }

        [Display(Name = "Icono")]
        [StringLength(256)]
        public string Icono { get; set; }

        [Display(Name = "URL")]
        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$|#|^(\w+)\/(\w+)?[\/(\w+).]+$",
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "URL")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = "Requerido")]
        [StringLength(256)]
        public string Url { get; set; }

        [Display(Name = "Habilitado")]
        public bool Activo { get; set; }

        [GridColumn(Title = "Estado")]
        public string ActivoString { get; set; }

    }
}