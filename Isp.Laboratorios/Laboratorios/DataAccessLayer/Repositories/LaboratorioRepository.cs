using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class LaboratorioRepository : IRepository<Laboratorio>
    {
        private readonly LaboratorioEntities _db;
        public LaboratorioRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Laboratorio entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Laboratorio entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Laboratorio entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Laboratorio> BuscarPor(Expression<Func<Laboratorio, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Laboratorio> ObtenerTodo()
        {

            return _db.Laboratorios.ToList();
        }

        public Laboratorio ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
        public List<Laboratorio> ObtenerPorUsuarioId(int usuarioId)
        {
            return new List<Laboratorio>(_db.UsuariosPorLaboratorios.Where(x => x.UsuarioId == usuarioId).Select(x => x.Laboratorio));
        }
        public Laboratorio ObtenerPorMuestraId(int muestraId)
        {
            var result = (from examen in _db.Examenes
                          join prestacion in _db.Prestaciones on examen.PrestacionId equals prestacion.Id
                          join laboratorio in _db.Laboratorios on prestacion.LaboratorioId equals laboratorio.Id
                          where examen.MuestraId == muestraId
                          select laboratorio).Distinct();

            return result.FirstOrDefault();
        }
    }
}