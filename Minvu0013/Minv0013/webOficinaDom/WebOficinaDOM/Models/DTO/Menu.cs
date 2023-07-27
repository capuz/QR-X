using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    public class Menu
    {

        public int IdMenu { get; set; }

        [StringLength(75, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CadenaLargo")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "Requerido")]
        [RegularExpression(@"^[\dA-zñÑá-úÁ-Ú.,_\-\s]+$",
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "CaracteresNoPermitidos")]
        public string Nombre { get; set; }

        [Display(Name = "URL")]
        [RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$|#|^(\w+)\/(\w+)?[\/(\w+).]+$",
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "URL")]
        [StringLength(256, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CadenaLargo")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "Requerido")]
        public string Url { get; set; }

        [Display(Name = "Destino")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "Requerido")]
        public int? Target { get; set; }

        public string Destino { get; set; }

        public string Dependencia { get; set; }

        [Display(Name = "Habilitado")]
        public bool Activo { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(2048, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "CadenaLargo")]
        public string Descripcion { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages),
         ErrorMessageResourceName = "Requerido")]
        [Display(Name = "Dependencia")]
        public int? IdMenuPadre { get; set; }

    }

}