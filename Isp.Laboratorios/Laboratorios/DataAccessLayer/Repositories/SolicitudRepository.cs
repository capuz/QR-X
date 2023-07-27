using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class SolicitudRepository : IRepository<Solicitud>
    {
        private readonly LaboratorioEntities _db;
        public SolicitudRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(Solicitud entity)
        {
            _db.Solicitudes.Add(entity);
        }

        public void Actualizar(Solicitud entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Eliminar(Solicitud entity)
        {
            _db.Solicitudes.Remove(entity);
        }

        public void EliminarPorId(int id)
        {
            _db.Entry(new Solicitud { Id = id }).State = EntityState.Deleted;
        }

        public List<Solicitud> BuscarPor(Expression<Func<Solicitud, bool>> predicate)
        {
            return _db.Solicitudes.Where(predicate).ToList();
        }

        public List<Solicitud> ObtenerTodo()
        {
            return _db.Solicitudes.ToList();
        }

        public Solicitud ObtenerPorId(int id)
        {
            return _db.Solicitudes.Find(id);
        }

    }
}