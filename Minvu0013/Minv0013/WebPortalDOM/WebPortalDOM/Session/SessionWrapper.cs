using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortalDOM.Session
{
    public class SessionWrapper
    {
        const string USUARIO = "usuario";

        public SessionWrapper(HttpSessionStateBase session)
            : this(session, typeof(SessionUsuario))
        {

        }

        public SessionWrapper(HttpSessionStateBase session, Type type)
        {
            this.Session = session;
            if (this.Session[SessionWrapper.USUARIO] == null)
            {
                ISessionUsuario usuario = new SessionUsuario();
                this.Session[SessionWrapper.USUARIO] = usuario;
            }
        }

        private HttpSessionStateBase Session { get; set; }

        public ISessionUsuario Usuario
        {
            get
            {

                return (ISessionUsuario)this.Session[SessionWrapper.USUARIO];
            }
        }

    }
}