using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class LimiteDeteccionRepository : IRepository<LimiteDeteccion>
    {
        private readonly LaboratorioEntities _db;
        public LimiteDeteccionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(LimiteDeteccion entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(LimiteDeteccion entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(LimiteDeteccion entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<LimiteDeteccion> BuscarPor(Expression<Func<LimiteDeteccion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<LimiteDeteccion> ObtenerTodo()
        {
            return _db.LimitesDeteccion.ToList();
        }

        public LimiteDeteccion ObtenerPorId(int id)
        {
            return _db.LimitesDeteccion.Find(id);
        }
    }
}