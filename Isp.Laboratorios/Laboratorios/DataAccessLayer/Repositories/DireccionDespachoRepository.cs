using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class DireccionDespachoRepository :IRepository<DireccionesDespacho>
    {
        private readonly LaboratorioEntities _db;
        public DireccionDespachoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(DireccionesDespacho entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(DireccionesDespacho entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(DireccionesDespacho entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<DireccionesDespacho> BuscarPor(Expression<Func<DireccionesDespacho, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<DireccionesDespacho> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public DireccionesDespacho ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}