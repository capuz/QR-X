using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class ComunaRepository:IRepository<Comuna>
    {
        private readonly LaboratorioEntities _db;
        public ComunaRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Comuna entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Comuna entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Comuna entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comuna> BuscarPor(Expression<Func<Comuna, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Comuna> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Comuna ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}