using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class ProvinciaRepository : IRepository<Provincia>
    {
        private readonly LaboratorioEntities _db;
        public ProvinciaRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Provincia entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Provincia entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Provincia entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Provincia> BuscarPor(Expression<Func<Provincia, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Provincia> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Provincia ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}