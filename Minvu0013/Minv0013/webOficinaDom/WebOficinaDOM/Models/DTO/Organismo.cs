using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models.DTO
{
    [GridTable(PagingEnabled = true, PageSize = 5)]
    public class Organismo
    {
        [NotMappedColumn]
        public int IdOrganismo { get; set; }

        [NotMappedColumn]
        [Display(Name = "Tipo")]
        [Required(ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "RequiredField_P1")]
        public int IdOrganismoTipo { get; set; }

        [NotMappedColumn]
        [Required(ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "RequiredField_P1")]
        [Display(Name = "Dependencia")]
        public int IdOrganismoPadre { get; set; }

        [Required(ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "RequiredField_P1")]
        [StringLength(75, ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [GridColumn(Title = "Nombre", SortEnabled = true, FilterEnabled = true)]
        [Display(Name = "Nombre")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[\dA-zñÑáéíóúÁÉÍÓÚ.,_\-\s]+$", ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "NotAllowChar_P0")]
        public string Nombre { get; set; }

        [NotMappedColumn]
        [StringLength(2048, ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[\dA-zñÑáéíóúÁÉÍÓÚ.,_\-\s]+$", ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "NotAllowChar_P0")]
        public string Descripcion { get; set; }

        [StringLength(75, ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [GridColumn(Title = "Tipo", SortEnabled = true, FilterEnabled = true)]
        [Display(Name = "Tipo")]
        [DataType(DataType.Text)]
        public string OrganismoTipo { get; set; }

        [StringLength(75, ErrorMessageResourceType = typeof(OrganismoResources), ErrorMessageResourceName = "MaxLenghtField_P0")]
        [GridColumn(Title = "Dependencia", SortEnabled = true, FilterEnabled = true)]
        [Display(Name = "Dependencia")]
        [DataType(DataType.Text)]
        public string OrganismoPadre { get; set; }

        [GridHiddenColumn]
        public string Icono { get; set; }

        [GridHiddenColumn]
        public string Logo { get; set; }

        [GridHiddenColumn]
        public IEnumerable<OrganismoTipo> OrganismoTipoList { get; set; }


        [GridHiddenColumn]
        public IEnumerable<OrganismoPadre> OrganismoPadreList { get; set; }

        [Required]
        [NotMappedColumn]
        public bool Activo { get; set; }

        [GridColumn(Title = "Estado", SortEnabled = true, FilterEnabled = true)]
        public String ActivoSt
        {
            get
            {
                return this.Activo == true ? "Activo" : "Desactivo";
            }
        }
    }
}