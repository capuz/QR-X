//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webApiDom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Noticia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Noticia()
        {
            this.Noticia_Galeria = new HashSet<Noticia_Galeria>();
        }
    
        public decimal IdNoticia { get; set; }
        public string Titular { get; set; }
        public string Bajada { get; set; }
        public string Cuerpo { get; set; }
        public string Imagen { get; set; }
        public Nullable<bool> Destacada { get; set; }
        public Nullable<decimal> Orden { get; set; }
        public Nullable<System.DateTime> FechaPublicacion { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Noticia_Galeria> Noticia_Galeria { get; set; }
    }
}