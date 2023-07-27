using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    public class ContenidoLogo
    {
        public int IdContenidoLogo { get; set; }
        public string RutaFisica { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }

        [Required(ErrorMessageResourceType = typeof(SeccionResouces), ErrorMessageResourceName = "RequiredLogo_P0")]
        [WebOficinaDOM.ValidationAttributes.MaxFileSize(3 * 1024 * 1024, ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = "MaxFileSize")]
        [WebOficinaDOM.ValidationAttributes.FileExtensions("jpg,jpeg,png,gif", ErrorMessage = "Seleccione archivo de tipo .png .jpg o .gif")]
        public HttpPostedFileBase File { get; set; }
    }
}