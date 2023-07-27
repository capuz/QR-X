using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class SeccionRepository : IRepository<Seccion>
    {
        private readonly LaboratorioEntities _db;
        public SeccionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Seccion entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Seccion entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Seccion entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Seccion> BuscarPor(Expression<Func<Seccion, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Seccion> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Seccion ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

    }
}