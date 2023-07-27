using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    public class ContenidoSecundario
    {
        [GridHiddenColumn]
        public int IdContenidoSecundario { get; set; }
        
        [Required]
        [Display(Name = "URL")]
        [RegularExpression(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$|#|^(\w+)\/(\w+)?[\/(\w+).]+$",
        ErrorMessageResourceType = typeof(ValidationMessages),
        ErrorMessageResourceName = "URL")]
        [StringLength(256)]
        [GridColumn(Title = "URL", SortEnabled = true, FilterEnabled = true)]
        public string Url { get; set; }

        [Display(Name = "Habilitado")]
        [GridHiddenColumn]
        public bool Activo { get; set; }
    }
}