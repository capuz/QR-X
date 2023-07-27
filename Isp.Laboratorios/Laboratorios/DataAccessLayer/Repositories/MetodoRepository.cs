using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class MetodoRepository : IRepository<Metodo>
    {
        private readonly LaboratorioEntities _db;
        public MetodoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Metodo entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Metodo entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Metodo entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Metodo> BuscarPor(Expression<Func<Metodo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Metodo> ObtenerTodo()
        {
            return _db.Metodos.ToList();
        }

        public List<Metodo> ObtenerPorAnalitos(List<Analito> analitos)
        {
            var analitosId = analitos.Select(x => x.Id).ToList();
            var result = _db.MetodosPorAnalitos
                            .Where(x => analitosId.Contains(x.AnalitoId))
                            .Select(x => x.Metodo).Distinct();
            return result.ToList();
        }
        public Metodo ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}