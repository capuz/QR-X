using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure.DataAccessLayer
{
    public class SistemasRepository : IRepository<Sistema>
    {
        private readonly LaboratorioEntities db;
        public SistemasRepository(LaboratorioEntities dbContext) 
        {
            this.db = dbContext;
        }
        public void Insertar(Sistema sistema)
        {
            db.Sistemas.Add(sistema);
        }
        public void Actualizar(Sistema sistema)
        {
            db.Entry(sistema).State = EntityState.Modified;
        }
        public void Eliminar(Sistema sistema)
        {
            throw new NotImplementedException();
        }
        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }
        public Sistema ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
        public List<Sistema> BuscarPor(Expression<Func<Sistema, bool>> predicate)
        {
            return db.Sistemas.Where(predicate).ToList();
        }
        public List<Sistema> ObtenerTodo()
        {
            return db.Sistemas.ToList();
        }
    }
}