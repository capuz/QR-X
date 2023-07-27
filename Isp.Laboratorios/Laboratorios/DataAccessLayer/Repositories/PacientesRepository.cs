using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isp.Laboratorios.DataAccessLayer
{
    public class PacientesRepository : IRepository<Paciente>
    {
        private readonly LaboratorioEntities _db;
        public PacientesRepository(Models.LaboratorioEntities _dbContext)
        {
            this._db = _dbContext;
        }

        public void Insertar(Paciente entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Paciente entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Paciente entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Paciente> BuscarPor(System.Linq.Expressions.Expression<Func<Paciente, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Paciente> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public Paciente ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
