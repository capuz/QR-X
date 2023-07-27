using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class PrestacionRepository : IRepository<Prestacion>
    {
        private readonly LaboratorioEntities _db;
        public PrestacionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Prestacion entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Prestacion entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Prestacion entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Prestacion> BuscarPor(Expression<Func<Prestacion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Prestacion> ObtenerTodo()
        {
            return _db.Prestaciones.ToList();
        }
        public List<Prestacion> ObtenerPorIds(List<int> id)
        {
            return _db.Prestaciones.Where(x=>id.Contains(x.Id)).ToList();
        }
        public Prestacion ObtenerPorId(int id)
        {
            return _db.Prestaciones.Find(id);
        }
    }
}