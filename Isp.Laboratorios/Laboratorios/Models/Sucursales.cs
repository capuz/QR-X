//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Isp.Laboratorios.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sucursales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sucursales()
        {
            this.Unidades = new HashSet<Unidades>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int ContactoCodigo { get; set; }
        public int ProcedenciaId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int ComunaId { get; set; }
    
        public virtual Comuna Comuna { get; set; }
        public virtual Procedencia Procedencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidades> Unidades { get; set; }
    }
}