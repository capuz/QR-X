using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOficinaDOM.Session
{
    public class SessionFactory
    {
        public static ISessionUsuario ObtenerUsuario(HttpSessionStateBase session)
        {
            SessionWrapper sessionWrapper = new SessionWrapper(session);

            return sessionWrapper.Usuario;
        }

        public static ISessionUsuario AsignarUsuario(HttpSessionStateBase session, int rut, string nombre)
        {
            SessionWrapper sessionWrapper = new SessionWrapper(session, typeof(Imp.SessionUsuario));
            ISessionUsuario usuario = sessionWrapper.Usuario;
            usuario.Asignar(rut,nombre);
            return usuario;
        }
    }
}