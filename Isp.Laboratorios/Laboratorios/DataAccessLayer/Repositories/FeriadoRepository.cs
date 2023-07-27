using System;
using System.Collections.Generic;
using System.Linq;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class FeriadoRepository
    {
        private readonly LaboratorioEntities _db;
        public FeriadoRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public IEnumerable<DateTime> ObtenerPorRangoFecha(DateTime desde, DateTime hasta)
        {
            return _db.Feriados.Where(x => x.Fecha >= desde && x.Fecha <= hasta).Select(x => x.Fecha).ToArray();
        }
    }
}