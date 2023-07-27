using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class LimiteMaximoPermitidoRepository : IRepository<LimiteMaximoPermitido>
    {
        private readonly LaboratorioEntities _db;
        public LimiteMaximoPermitidoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(LimiteMaximoPermitido entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(LimiteMaximoPermitido entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(LimiteMaximoPermitido entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<LimiteMaximoPermitido> BuscarPor(Expression<Func<LimiteMaximoPermitido, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<LimiteMaximoPermitido> ObtenerTodo()
        {
            return _db.LimitesMaximosPermitidos.ToList();
        }

        public LimiteMaximoPermitido ObtenerPorId(int id)
        {
            return _db.LimitesMaximosPermitidos.Find(id);
        }
    }
}