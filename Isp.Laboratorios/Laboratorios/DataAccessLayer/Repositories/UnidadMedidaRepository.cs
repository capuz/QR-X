using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class UnidadMedidaRepository : IRepository<UnidadMedida>
    {
        private readonly LaboratorioEntities _db;
        public UnidadMedidaRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(UnidadMedida entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(UnidadMedida entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(UnidadMedida entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<UnidadMedida> BuscarPor(Expression<Func<UnidadMedida, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<UnidadMedida> ObtenerTodo()
        {
            return _db.UnidadesMedidas.ToList();
        }

        public UnidadMedida ObtenerPorId(int id)
        {
            return _db.UnidadesMedidas.Find(id);
        }
    }
}