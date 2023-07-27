using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using System.Web;
using System.Linq;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class EstadoRepository : IRepository<Estado>
    {
        private readonly LaboratorioEntities _db;
        public EstadoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(Estado entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Estado entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Estado entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Estado> BuscarPor(Expression<Func<Estado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Estado> ObtenerTodo()
        {
            return _db.Estados.ToList();
        }

        public Estado ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}