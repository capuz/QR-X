using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.Authentication;
using System.Data.Entity;
using Rol = Isp.Laboratorios.Models.Enums.Rol;

namespace Isp.Laboratorios.Infrastructure.DataAccessLayer
{
    public class UsuariosRepository : IRepository<Usuario>
    {
        private readonly LaboratorioEntities _db;
        public UsuariosRepository(LaboratorioEntities dbContext)
        {
            this._db = dbContext;
        }

        public void Insertar(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
        }
        public void Actualizar(Usuario usuario)
        {
            _db.Entry(usuario).State = EntityState.Modified;
        }
        public void Eliminar(Usuario sistema)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario ObtenerPorId(int id)
        {
            return _db.Usuarios.Find(id);
        }

        public List<Usuario> BuscarPor(Expression<Func<Usuario, bool>> predicate)
        {
            return _db.Usuarios.Where(predicate).ToList();
        }
        public List<Usuario> ObtenerTodo()
        {
            return _db.Usuarios.ToList();
        }
        public List<Usuario> ObtenerUsuario(Login login)
        {
            var result = from u in _db.Usuarios
                         join s in _db.Sistemas on u.UsuariosPorSistema.SistemaId equals s.Id
                         where u.NombreUsuario == login.UserName
                               && u.Contrasena == login.Password
                               && u.Activo && s.Id == 1//Laboratorio Salud Ambiental
                         select u;

            return result.ToList();
        }
        public Usuario ObtenerPorNombre(string userName)
        {
            return _db.Usuarios.FirstOrDefault(u => u.NombreUsuario == userName);
        }
        public Usuario ObtenerPorCorreo(string correo)
        {
            return _db.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correo);
        }
        public Usuario ObtenerResponsableRtm()
        {
            var result = (from usuario in _db.Usuarios
                where usuario.RolId == (int) Rol.ResponsableTomaDeMuestra
                orderby usuario.Id descending
                select usuario);


            return result.FirstOrDefault();
        }
    }
}