using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class MotivoRechazoRepository : IRepository<MotivoRechazo>
    {
        private readonly LaboratorioEntities _db;
        public MotivoRechazoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(MotivoRechazo entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(MotivoRechazo entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(MotivoRechazo entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<MotivoRechazo> BuscarPor(Expression<Func<MotivoRechazo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<MotivoRechazo> ObtenerTodo()
        {
            throw new NotImplementedException();
        }

        public MotivoRechazo ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<MotivoRechazo> ObtenerPorMuestraId(List<int> muestrasId)
        {
            var result = (from examen in _db.Examenes
                          join prestacion in _db.Prestaciones on examen.PrestacionId equals prestacion.Id
                          join laboratorio in _db.Laboratorios on prestacion.LaboratorioId equals laboratorio.Id
                          join motivoslab in _db.MotivosRechazoPorLaboratorios on laboratorio.Id equals motivoslab.LaboratorioId
                          where muestrasId.Contains(examen.MuestraId)
                          select motivoslab.MotivoRechazo).Distinct();

            return result.ToList();

        }
    }
}