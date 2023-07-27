using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure.DataAccessLayer
{
    public class FuncionalidadesRepository : IRepository<Funcionalidad>
    {
        private readonly LaboratorioEntities _db;
        public FuncionalidadesRepository(LaboratorioEntities dbContext)
        {
            this._db = dbContext;
        }

        public void Insertar(Funcionalidad funcionalidad)
        {
            _db.Funcionalidades.Add(funcionalidad);
        }
        public void Actualizar(Funcionalidad funcionalidad)
        {
            _db.Funcionalidades.Attach(funcionalidad);
            _db.Entry(funcionalidad).State = EntityState.Modified;
        }
        public void Eliminar(Funcionalidad sistema)
        {
            _db.Funcionalidades.Remove(sistema);
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Funcionalidad ObtenerPorId(int id)
        {
            return _db.Funcionalidades.Find(id);
        }

        public List<Funcionalidad> BuscarPor(Expression<Func<Funcionalidad, bool>> predicate)
        {
            return _db.Funcionalidades.Where(predicate).ToList();
        }
        public List<Funcionalidad> ObtenerTodo()
        {
            return _db.Funcionalidades.ToList();
        }
        public List<Funcionalidad> ObtenerFuncionalidades(Usuario usuario)
        {
            //var result = from u in _db.Usuarios
            //             join r in _db.Roles on u.RolId equals r.Id
            //             join fr in _db.FuncionalidadesPorRol on r.Id equals fr.RolId
            //             join f in _db.Funcionalidades on fr.FuncionalidadId equals f.Id
            //             where u.Id == usuarioId
            //             select f;
            return usuario.Rol.FuncionalidadesPorRol.Select(f => f.Funcionalidad).ToList();

        }
        public List<Funcionalidad> ObtenerFuncionalidadesPadre(int? usuarioId)
        {

            var result = from u in _db.Usuarios
                         join r in _db.Roles on u.RolId equals r.Id
                         join fr in _db.FuncionalidadesPorRol on r.Id equals fr.RolId
                         join f in _db.Funcionalidades on fr.FuncionalidadId equals f.Id
                         where u.Id == usuarioId
                             && f.Visible
                             && r.Activo
                             && u.Activo
                             && f.FuncionalidadPadre == null
                         orderby f.Orden ascending
                         select f;

            return result.ToList();
        }
        public List<Funcionalidad> ObtenerFuncionalidadesHija(int? usuarioId, int? funcionalidadPadreId)
        {
            var result = from u in _db.Usuarios
                         join r in _db.Roles on u.RolId equals r.Id
                         join fr in _db.FuncionalidadesPorRol on r.Id equals fr.RolId
                         join f in _db.Funcionalidades on fr.FuncionalidadId equals f.Id
                         where u.Id == usuarioId
                             && f.Visible
                             && r.Activo
                             && u.Activo
                             && f.FuncionalidadPadreId == funcionalidadPadreId
                         orderby f.Orden ascending
                         select f;

            return result.ToList();
        }
        public Funcionalidad ObtenerFuncionalidad(string controlador, string accion)
        {
            return _db.Funcionalidades.FirstOrDefault(f => f.Controlador == controlador && f.Accion == accion && f.FuncionalidadPadre != null);
        }
    }
}