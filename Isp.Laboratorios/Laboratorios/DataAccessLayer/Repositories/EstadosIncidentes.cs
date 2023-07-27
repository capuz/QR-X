using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using System.Web;
using System.Linq;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class EstadoIncidenteRepository : IRepository<EstadoIncidente>
    {
        private readonly LaboratorioEntities _db;
        public EstadoIncidenteRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(EstadoIncidente entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(EstadoIncidente entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(EstadoIncidente entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<EstadoIncidente> BuscarPor(Expression<Func<EstadoIncidente, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<EstadoIncidente> ObtenerTodo()
        {
            return _db.EstadosIncidentes.ToList();
        }

        public EstadoIncidente ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}