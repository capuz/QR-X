using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    [GridTable(PagingEnabled = true, PageSize = 5)]
    public class Noticia
    {
        [GridHiddenColumn]
        public int IdNoticia { get; set; }

        [Required(ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "RequiredField_P1")]
        [StringLength(100,ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [GridColumn(Title = "Título", SortEnabled = true, FilterEnabled = true)]
        [Display(Name = "Título")]
        [DataType(DataType.MultilineText)]
        public string Titular { get; set; }

        [Required(ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "RequiredField_P1")]
        [StringLength(250, ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [Display(Name = "Resumen")]
        [NotMappedColumn]
        [DataType(DataType.MultilineText)]
        public string Bajada { get; set; }

        [Required(ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "RequiredField_P1")]
        [StringLength(8000, ErrorMessageResourceType = typeof(NoticiaResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [NotMappedColumn]
        [Display(Name = "Noticia")]
        [DataType(DataType.MultilineText)]
        public string Cuerpo { get; set; }

        [NotMappedColumn]
        public string Imagen { get; set; }

        [Required]
        [NotMappedColumn]
        public bool Destacada { get; set; }

        [Required]
        [NotMappedColumn]
        public bool Activo { get; set; }

        [GridColumn(Title = "Activo", SortEnabled = true, FilterEnabled = true)]
        public String ActivoSt {
            get {
                return this.Activo == true ? "Activo" : "Desactivo";
            }
        }

        [GridColumn(Title = "Destacada", SortEnabled = true, FilterEnabled = true)]
        public String DestacadaSt
        {
            get
            {
                return this.Activo == true ? "Si" : "No";
            }
        }

        [GridColumn(Title = "Fecha de publicación",Format = "{0:dd/MM/yyyy}", SortEnabled = true, FilterEnabled = true)]
        public DateTime? FechaPublicacion { get; set; }

        [NotMappedColumn]
        public DateTime FechaCreacion { get; set; }

    }
}