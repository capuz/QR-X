using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using System.Data.Entity;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class MuestraObservacionRepository : IRepository<MuestraObservacion>
    {
        private readonly LaboratorioEntities _db;
        public MuestraObservacionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(MuestraObservacion entity)
        {
            _db.MuestraObservaciones.Add(entity);
        }

        public void Actualizar(MuestraObservacion entity )
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Eliminar(MuestraObservacion entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<MuestraObservacion> BuscarPor(Expression<Func<MuestraObservacion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<MuestraObservacion> ObtenerTodo()
        {
            return _db.MuestraObservaciones.ToList();
        }
        public MuestraObservacion ObtenerPorId(int Id)
        {
            return _db.MuestraObservaciones.Where(x => x.Id == Id).FirstOrDefault();
        }
        public List<MuestraObservacion> ObtenerPorEstadoIncidenteId(int id)
        {
            return _db.MuestraObservaciones.Where(x=> x.EstadoIncidenteId == id).ToList();
        }
        public List<MuestraObservacion> ObtenerPorMuestraId(int muestraId)
        {
            return _db.MuestraObservaciones.Where(x => x.MuestraId == muestraId).ToList();
        }
    }
}