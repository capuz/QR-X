using System;
using System.Collections.Generic;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class BitacoraResultadoAnalitoRepository
    {
        private readonly LaboratorioEntities _db;
        public BitacoraResultadoAnalitoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }

        public void Insertar(IEnumerable<BitacoraResultadoAnalito> bitacoraList, int usuarioId)
        {
            foreach (var bitacora in bitacoraList)
            {
                bitacora.UsuarioId = usuarioId;
                _db.BitacoraResultadosAnalitos.Add(bitacora);
            }
        }

        //public void Insertar(IEnumerable<ResultadoAnalito> resultadoAnalito, int usuarioId)
        //{

        //    //foreach (var bitacora in resultadoAnalito.Select(b => new BitacoraResultadoAnalito()
        //    //{
        //    //    AnalitoId = b.AnalitoId,
        //    //    ExamenId = b.ExamenId,
        //    //    Fecha = DateTime.Now,
        //    //    Informar = b.Informar,
        //    //    LimiteCuantificacionId = b.LimiteCuantificacionId,
        //    //    LimiteDeteccionId = b.LimiteDeteccionId,
        //    //    LimiteMaximoPermitidoId = b.LimiteMaximoPermitidoId,
        //    //    MetodoId = b.MetodoId,
        //    //    NormaId = b.NormaId,
        //    //    UnidadMedidaId = b.UnidadMedidaId,
        //    //    UsuarioId = usuarioId
        //    //}))
        //    //{
        //    //    _db.BitacoraResultadosAnalitos.Add(bitacora);
        //    //}
        //}
    }
}