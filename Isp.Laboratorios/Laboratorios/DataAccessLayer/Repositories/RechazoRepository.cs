using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class RechazoRepository : IRepository<Rechazo>
    {
        private readonly LaboratorioEntities _db;

        public RechazoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(Rechazo entity)
        {
            _db.Rechazos.Add(entity);
        }

        public void Actualizar(Rechazo entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Eliminar(Rechazo entity)
        {
            _db.Rechazos.Remove(entity);
        }

        public void EliminarPorId(int id)
        {
            _db.Entry(new Rechazo { Id = id }).State = EntityState.Deleted;
        }

        public List<Rechazo> BuscarPor(Expression<Func<Rechazo, bool>> predicate)
        {
            return _db.Rechazos.Where(predicate).ToList();
        }

        public List<Rechazo> ObtenerTodo()
        {
            return _db.Rechazos.ToList();
        }

        public Rechazo ObtenerPorId(int id)
        {
            return _db.Rechazos.Find(id);
        }
        public List<Rechazo> ObtenerPorMuestraId(int muestraId)
        {
            var result = _db.Rechazos.Where(x => x.Examen.MuestraId == muestraId);

            return result.ToList();
        }
    }
}