using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class DepartamentoRepository:IRepository<Departamento>
    {
        private readonly LaboratorioEntities _db;
        public DepartamentoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Departamento entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Departamento entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Departamento entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Departamento> BuscarPor(Expression<Func<Departamento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Departamento> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Departamento ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}