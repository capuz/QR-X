using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Session
{
    public class SessionUsuario :ISessionUsuario
    {
        public SessionUsuario()
        {
            this.Limpiar();
        }

        public int Rut { get; set; }
        public string Usuario { get; set; }

        public void Limpiar()
        {
            this.Rut = default(int);
            this.Usuario = string.Empty;
        }

        public void Asignar(int rut, string usuario)
        {
            this.Rut = rut;
            this.Usuario = usuario;
        }
    }
}