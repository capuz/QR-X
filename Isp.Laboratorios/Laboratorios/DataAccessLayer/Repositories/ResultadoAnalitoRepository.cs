using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class ResultadoAnalitoRepository : IRepository<ResultadoAnalito>
    {
        private readonly LaboratorioEntities _db;
        public ResultadoAnalitoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(ResultadoAnalito entity)
        {
            _db.ResultadosAnalitos.Add(entity);
        }
        public void Insertar(IEnumerable<ResultadoAnalito> entitys)
        {
            foreach (var entity in entitys)
            {
                _db.ResultadosAnalitos.Add(entity);
            }
        }
        public void Actualizar(ResultadoAnalito entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(ResultadoAnalito entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            var ra = new ResultadoAnalito { Id = id };
            _db.ResultadosAnalitos.Attach(ra);
            _db.ResultadosAnalitos.Remove(ra);
        }

        public List<ResultadoAnalito> BuscarPor(Expression<Func<ResultadoAnalito, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<ResultadoAnalito> ObtenerTodo()
        {
            return _db.ResultadosAnalitos.ToList();
        }

        public ResultadoAnalito ObtenerPorId(int id)
        {
            return _db.ResultadosAnalitos.Find(id);
        }
        public List<ResultadoAnalito> ObtenerPorExamenIds(List<int> examenIds)
        {
            return _db.ResultadosAnalitos
                      .Where(x => examenIds.Contains(x.ExamenId)).ToList();
        }
    }
}