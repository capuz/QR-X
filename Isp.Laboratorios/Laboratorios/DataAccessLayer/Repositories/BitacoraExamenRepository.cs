using System;
using System.Collections.Generic;
using System.Linq;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class BitacoraExamenRepository
    {
        private readonly LaboratorioEntities _db;
        public BitacoraExamenRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public void Insertar(IEnumerable<Examen> examenes, int usuarioId)
        {
            foreach (var bitacora in examenes.Select(examen => new BitacoraExamenes
            {
                Correlativo = examen.Correlativo,
                DespachoClienteId = examen.DespachoClienteId,
                DespachoLaboratorioId = examen.DespachoLaboratorioId,
                DespachoRtmId = examen.DespachoRtmId,
                EstadoId = examen.EstadoId,
                ExamenId = examen.Id,
                Fecha = DateTime.Now,
                MuestraId = examen.MuestraId,
                Observacion = examen.Observacion,
                PrestacionId = examen.PrestacionId,
                UsuarioId = usuarioId
            }))
            {
                _db.BitacoraExamenes.Add(bitacora);
            }
        }
    }
}