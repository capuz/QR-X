using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class LimiteCuentificacionRepository : IRepository<LimiteCuantificacion>
    {
        private readonly LaboratorioEntities _db;
        public LimiteCuentificacionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(LimiteCuantificacion entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(LimiteCuantificacion entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(LimiteCuantificacion entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<LimiteCuantificacion> BuscarPor(Expression<Func<LimiteCuantificacion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<LimiteCuantificacion> ObtenerTodo()
        {
            return _db.LimitesCuantificaciones.ToList();
        }

        public LimiteCuantificacion ObtenerPorId(int id)
        {
            return _db.LimitesCuantificaciones.Find(id);
        }
    }
}