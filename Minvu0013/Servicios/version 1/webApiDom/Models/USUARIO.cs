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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.USUARIO_PERFIL = new HashSet<USUARIO_PERFIL>();
        }
    
        public decimal IdUsuario { get; set; }
        public Nullable<decimal> IdOrganismo { get; set; }
        public Nullable<decimal> Rut { get; set; }
        public string RutDv { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
        public Nullable<byte> Activo { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<decimal> UsuarioCreacion { get; set; }
        public Nullable<decimal> UsuarioModificacion { get; set; }
    
        public virtual ORGANISMO ORGANISMO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO_PERFIL> USUARIO_PERFIL { get; set; }
    }
}