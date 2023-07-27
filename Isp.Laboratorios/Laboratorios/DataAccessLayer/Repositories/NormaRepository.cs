using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class NormaRepository : IRepository<Norma>
    {
        private readonly LaboratorioEntities _db;
        public NormaRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(Norma entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Norma entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Norma entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Norma> BuscarPor(Expression<Func<Norma, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Norma> ObtenerTodo()
        {
            return _db.Normas.ToList();
        }

        public Norma ObtenerPorId(int id)
        {
            return _db.Normas.Find(id);
        }

    }
}