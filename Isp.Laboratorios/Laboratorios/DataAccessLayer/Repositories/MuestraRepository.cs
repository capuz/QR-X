using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class MuestraRepository : IRepository<Muestra>
    {
        private readonly LaboratorioEntities _db;
        public MuestraRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(Muestra entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Muestra entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Muestra entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Muestra> BuscarPor(Expression<Func<Muestra, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Muestra> ObtenerTodo()
        {
            throw new NotImplementedException();
        }
        public List<Muestra> Obtener(FiltroBusqueda filtroBusqueda)
        {


            var result = (from ex in _db.Examenes
                          join muestra in _db.Muestras on ex.MuestraId equals muestra.Id
                          join solicitud in _db.Solicitudes on muestra.SolicitudId equals solicitud.Id
                          join estado in _db.Estados on ex.EstadoId equals estado.Id
                          join prestacion in _db.Prestaciones on ex.PrestacionId equals prestacion.Id
                          join laboratorio in _db.Laboratorios on prestacion.LaboratorioId equals laboratorio.Id
                          join usrLab in _db.UsuariosPorLaboratorios on laboratorio.Id equals usrLab.LaboratorioId
                          where ex.EstadoId == filtroBusqueda.ExamenEstado
                           && (filtroBusqueda.LaboratoriosId.Contains(prestacion.LaboratorioId) || !filtroBusqueda.LaboratoriosId.Any())
                           && (solicitud.Codigo == filtroBusqueda.SolicitudCodigo || filtroBusqueda.SolicitudCodigo == null)
                           && (solicitud.Fecha >= filtroBusqueda.FechaIngresoDesde || filtroBusqueda.FechaIngresoDesde == null)
                           && (solicitud.Fecha <= filtroBusqueda.FechaIngresoHasta || filtroBusqueda.FechaIngresoHasta == null)
                           && (muestra.Codigo == filtroBusqueda.MuestraCodigo || filtroBusqueda.MuestraCodigo == null)
                          orderby muestra.Solicitud.Fecha
                          select

                          ex.Muestra).Distinct();

            return result.ToList();
        }
        public Muestra ObtenerPorId(int id)
        {
            return _db.Muestras.Find(id);
        }
        public Muestra ObtenerDetallePorId(int id)
        {
            return _db.Muestras.Include(x=>x.Solicitud.DireccionesDespacho.Comuna.Provincia.Region).FirstOrDefault(x=>x.Id==id);
        }
        public string ObtenerCodigoPorId(int id)
        {
            return _db.Muestras.Select(x => x.Codigo).FirstOrDefault();
        }

    }
}