using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class SubdepartamentoRepository : IRepository<Subdepartamento>
    {
         private readonly LaboratorioEntities _db;
         public SubdepartamentoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Subdepartamento entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Subdepartamento entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Subdepartamento entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Subdepartamento> BuscarPor(Expression<Func<Subdepartamento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Subdepartamento> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Subdepartamento ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

    }
}