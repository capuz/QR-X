using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class RegionRepository:IRepository<Region>
    {
         private readonly LaboratorioEntities _db;
         public RegionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Region entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Region entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Region entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Region> BuscarPor(Expression<Func<Region, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Region> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Region ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}