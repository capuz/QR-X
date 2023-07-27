using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class AnalitoRepository : IRepository<Analito>
    {
        private readonly LaboratorioEntities _db;
        public AnalitoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Analito entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Analito entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Analito entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Analito> BuscarPor(Expression<Func<Analito, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Analito> ObtenerTodo()
        {
            return _db.Analitos.ToList();
        }

        public Analito ObtenerPorId(int id)
        {
            return _db.Analitos.Find(id);
        }
        public List<Analito> ObtenerPorPrestaciones(IEnumerable<Prestacion> prestaciones)
        {
            var prestacionIds = prestaciones.Select(p => p.Id).ToList();
            var result = _db.AnalitosPorPrestaciones
                            .Where(x => prestacionIds.Contains(x.PrestacionId))
                            .Select(x => x.Analito).Distinct();
            return result.ToList();
        }
        public List<Analito> ObtenerPorPrestacionId(int prestacionId)
        {
            var result = _db.AnalitosPorPrestaciones
                            .Where(x => x.PrestacionId == prestacionId)
                            .Select(x => x.Analito).Distinct();
            return result.ToList();
        }

    }
}