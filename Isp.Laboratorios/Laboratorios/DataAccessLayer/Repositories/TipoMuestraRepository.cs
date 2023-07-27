using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class TipoMuestraRepository : IRepository<TipoMuestra>
    {

        private readonly LaboratorioEntities _db;
        public TipoMuestraRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(TipoMuestra entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(TipoMuestra entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(TipoMuestra entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<TipoMuestra> BuscarPor(Expression<Func<TipoMuestra, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TipoMuestra> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public TipoMuestra ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}