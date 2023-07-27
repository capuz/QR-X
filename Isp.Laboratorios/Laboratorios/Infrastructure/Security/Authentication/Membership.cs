using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Isp.Laboratorios.DataAccessLayer;
using Isp.Laboratorios.Infrastructure.Security.Encrypting;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Authentication;

namespace Isp.Laboratorios.Infrastructure.Security.Authentication
{
    public static class Membership
    { 
        private static readonly UnitOfWork Db = new UnitOfWork();

        public static bool ValidarUsuario(Login login)
        {
     
            login.Password = Sha1.Encrypt(login.Password);
            var result = Db.Usuarios.ObtenerUsuario(login);

            return result.Any();
        }
        public static List<Funcionalidad> ObtenerFuncionalidades(int? usuarioId)
        {
            Db.DbContext.Configuration.LazyLoadingEnabled = false;
            var funcionalidades = Db.Funcionalidades.ObtenerFuncionalidadesPadre(usuarioId);

            foreach (var funcionalidad in funcionalidades)
            {
                funcionalidad.Funcionalidades = Db.Funcionalidades.ObtenerFuncionalidadesHija(usuarioId, funcionalidad.Id);
            }

            return funcionalidades;
        }
        public static string ObtenerNombreUsuarioCompleto()
        {
            var usuario = ApplicationInfo.CurrentUser;
            return String.Format("{0} {1} {2}", usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno);
        }
    }
}