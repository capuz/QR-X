using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.FiltrosEstadisticas;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class ExamenRepository : IRepository<Examen>
    {
        private readonly LaboratorioEntities _db;
        public ExamenRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(Examen entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Examen entity)
        {
            throw new NotImplementedException();
        }
        public void Actualizar(List<Examen> examenes)
        {
            foreach (var examen in examenes) _db.Entry(examen).State = EntityState.Modified;
        }
        public void Eliminar(Examen entity)
        {
            throw new NotImplementedException();
        }

        public void EliminarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Examen> BuscarPor(Expression<Func<Examen, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Examen> ObtenerTodo()
        {
            return _db.Examenes.ToList();
        }

        public Examen ObtenerPorId(int id)
        {
            return _db.Examenes.Find(id);
        }
        public List<Examen> ObtenerPorIds(List<int> ids)
        {
          return  _db.Examenes.Where(x => ids.Contains(x.Id))
                     .Select(x => x)
                     .ToList();
        }
        public List<Examen> ObtenerPorMuestraLaboratorio(List<int> muestrasId, List<int> laboratoriosId)
        {

            var result = (from muestra in _db.Muestras
                          join examen in _db.Examenes on muestra.Id equals examen.MuestraId
                          join prestacion in _db.Prestaciones on examen.PrestacionId equals prestacion.Id
                          join laboratorio in _db.Laboratorios on prestacion.LaboratorioId equals laboratorio.Id
                          join usrLab in _db.UsuariosPorLaboratorios on laboratorio.Id equals usrLab.LaboratorioId
                          where (muestrasId.Contains(muestra.Id))
                          && (laboratoriosId.Contains(prestacion.LaboratorioId) || !laboratoriosId.Any())
                          select examen);

            return result.ToList();


        }



        public List<Examen> Obtener(FiltroBusquedaGeneral filtroBusquedaGeneral)
        {
            //if (filtroBusquedaGeneral.NumeroFormulario == 0)
            //   filtroBusquedaGeneral.NumeroFormulario = "null";

            var result = (from solicitud in _db.Solicitudes
                          join muestra in _db.Muestras on solicitud.Id equals muestra.SolicitudId
                          join examen in _db.Examenes on muestra.Id equals examen.MuestraId
                          join prestac in _db.Prestaciones on examen.PrestacionId equals prestac.Id
                          join laboratorio in _db.Laboratorios on prestac.LaboratorioId equals laboratorio.Id
                          join estadoExamen in _db.Estados on examen.EstadoId equals estadoExamen.Id
                          join usrLab in _db.UsuariosPorLaboratorios on laboratorio.Id equals usrLab.LaboratorioId
                          join estado in _db.Estados on examen.EstadoId equals estado.Id
                          where (filtroBusquedaGeneral.CodigoSolicitud == solicitud.Codigo || filtroBusquedaGeneral.CodigoSolicitud == null)
                          && (filtroBusquedaGeneral.NumeroFormulario == solicitud.NumeroFormulario || filtroBusquedaGeneral.NumeroFormulario == 0)
                          && (filtroBusquedaGeneral.NombreLaboratorio == prestac.Laboratorio.Nombre || filtroBusquedaGeneral.NombreLaboratorio == null)
                          && (filtroBusquedaGeneral.CodigoMuestra == muestra.Codigo || filtroBusquedaGeneral.CodigoMuestra == null)
                          && (filtroBusquedaGeneral.PrestacionId == examen.Prestacion.Id || filtroBusquedaGeneral.PrestacionId == null)
                          //&& (filtroBusquedaGeneral.NumeroComercializacion == solicitud.NumeroComercializacion || filtroBusquedaGeneral.NumeroComercializacion == 0)
                          && (filtroBusquedaGeneral.ListadoNumero == examen.DespachoLaboratorioId || filtroBusquedaGeneral.ListadoNumero == null)
                          && (filtroBusquedaGeneral.ExamenEstado == examen.EstadoId || filtroBusquedaGeneral.ExamenEstado == null)
                          && (solicitud.Fecha >= filtroBusquedaGeneral.FechaIngresoDesde || filtroBusquedaGeneral.FechaIngresoDesde == null)
                          && (solicitud.Fecha <= filtroBusquedaGeneral.FechaIngresoHasta || filtroBusquedaGeneral.FechaIngresoHasta == null)
                          //&& (filtroBusquedaGeneral.FechaIngresoDesde >= solicitud.Fecha || filtroBusquedaGeneral.FechaIngresoDesde == null)
                          //&& (filtroBusquedaGeneral.FechaIngresoHasta <= solicitud.Fecha || filtroBusquedaGeneral.FechaIngresoHasta == null)
                          orderby solicitud.Fecha
                          select examen);

            return result.ToList();
        }


        public List<Examen> Obtener(FiltroEstadistica filtroEstadistica)
        {
            var result = (from solicitud in _db.Solicitudes
                          join muestra in _db.Muestras on solicitud.Id equals muestra.SolicitudId
                          join examen in _db.Examenes on muestra.Id equals examen.MuestraId
                          join prestac in _db.Prestaciones on examen.PrestacionId equals prestac.Id
                          join laboratorio in _db.Laboratorios on prestac.LaboratorioId equals laboratorio.Id
                          join estadoExamen in _db.Estados on examen.EstadoId equals estadoExamen.Id
                          join usrLab in _db.UsuariosPorLaboratorios on laboratorio.Id equals usrLab.LaboratorioId
                          join estado in _db.Estados on examen.EstadoId equals estado.Id
                          where (filtroEstadistica.CodigoSolicitud == solicitud.Codigo || filtroEstadistica.CodigoSolicitud == null)
                          && (filtroEstadistica.NumeroFormulario == solicitud.NumeroFormulario || filtroEstadistica.NumeroFormulario == 0)
                          && (filtroEstadistica.NombreLaboratorio == prestac.Laboratorio.Nombre || filtroEstadistica.NombreLaboratorio == null)
                          && (filtroEstadistica.CodigoMuestra == muestra.Codigo || filtroEstadistica.CodigoMuestra == null)
                          && (filtroEstadistica.PrestacionId == examen.Prestacion.Id || filtroEstadistica.PrestacionId == null)
                              //&& (filtroEstadistica.NumeroComercializacion == solicitud.NumeroComercializacion || filtroEstadistica.NumeroComercializacion == 0)
                          && (filtroEstadistica.ListadoNumero == examen.DespachoLaboratorioId || filtroEstadistica.ListadoNumero == null)
                          && (filtroEstadistica.ExamenEstado == examen.EstadoId || filtroEstadistica.ExamenEstado == null)
                          && (solicitud.Fecha >= filtroEstadistica.FechaIngresoDesde || filtroEstadistica.FechaIngresoDesde == null)
                          && (solicitud.Fecha <= filtroEstadistica.FechaIngresoHasta || filtroEstadistica.FechaIngresoHasta == null)
                          //&& (filtroEstadistica.FechaIngresoDesde >= solicitud.Fecha || filtroEstadistica.FechaIngresoDesde == null)
                          //&& (filtroEstadistica.FechaIngresoHasta <= solicitud.Fecha || filtroEstadistica.FechaIngresoHasta == null)
                          orderby solicitud.Fecha
                          select examen);

            return result.ToList();
        }


        public List<Examen> ObtenerHistorico(FiltroBusquedaGeneral filtroBusquedaHistorica)
        {
            //if (filtroBusquedaHistorica.NumeroFormulario == 0)
            //   filtroBusquedaHistorica.NumeroFormulario = "null";

            var result = (from solicitud in _db.Solicitudes
                          join muestra in _db.Muestras on solicitud.Id equals muestra.SolicitudId
                          join examen in _db.Examenes on muestra.Id equals examen.MuestraId
                          join prestac in _db.Prestaciones on examen.PrestacionId equals prestac.Id
                          join laboratorio in _db.Laboratorios on prestac.LaboratorioId equals laboratorio.Id
                          join estadoExamen in _db.Estados on examen.EstadoId equals estadoExamen.Id
                          join usrLab in _db.UsuariosPorLaboratorios on laboratorio.Id equals usrLab.LaboratorioId
                          join bitacora in _db.BitacoraExamenes on examen.Id equals bitacora.ExamenId
                          join  inci in _db.MuestraObservaciones.DefaultIfEmpty() on muestra.Id equals inci.MuestraId
                          where (filtroBusquedaHistorica.CodigoSolicitud == solicitud.Codigo || filtroBusquedaHistorica.CodigoSolicitud == null)
                          && (filtroBusquedaHistorica.NumeroFormulario == solicitud.NumeroFormulario || filtroBusquedaHistorica.NumeroFormulario == 0)
                          && (filtroBusquedaHistorica.NombreLaboratorio == prestac.Laboratorio.Nombre || filtroBusquedaHistorica.NombreLaboratorio == null)
                          && (filtroBusquedaHistorica.CodigoMuestra == muestra.Codigo || filtroBusquedaHistorica.CodigoMuestra == null)
                          && (filtroBusquedaHistorica.PrestacionId == examen.Prestacion.Id || filtroBusquedaHistorica.PrestacionId == null)
                              //&& (filtroBusquedaGeneral.NumeroComercializacion == solicitud.NumeroComercializacion || filtroBusquedaGeneral.NumeroComercializacion == 0)
                          && (filtroBusquedaHistorica.ListadoNumero == examen.DespachoLaboratorioId || filtroBusquedaHistorica.ListadoNumero == null)
                          && (filtroBusquedaHistorica.ExamenEstado == examen.EstadoId || filtroBusquedaHistorica.ExamenEstado == null)
                          && (solicitud.Fecha >= filtroBusquedaHistorica.FechaIngresoDesde || filtroBusquedaHistorica.FechaIngresoDesde == null)
                          && (solicitud.Fecha <= filtroBusquedaHistorica.FechaIngresoHasta || filtroBusquedaHistorica.FechaIngresoHasta == null)
                          orderby solicitud.Fecha
                          select examen);
            return result.ToList();
        }

    }
}