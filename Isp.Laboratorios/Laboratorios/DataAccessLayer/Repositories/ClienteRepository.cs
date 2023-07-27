using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class ProcedenciaRepository : IRepository<Procedencia>
    {
        private readonly LaboratorioEntities _db;
        public ProcedenciaRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Procedencia entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Procedencia entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Procedencia entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Procedencia> BuscarPor(Expression<Func<Procedencia, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Procedencia> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Procedencia ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}