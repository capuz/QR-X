using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer.Repositories
{
    public class TipoObservacionRepository
    {
        private readonly LaboratorioEntities _db;
        public TipoObservacionRepository(LaboratorioEntities dbContext)
        {
            _db = dbContext;
        }
        public List<TipoObservacion> ObtenerTodo()
        {
            return _db.TiposObservaciones.Where(x => x.Activo).ToList();
        }
    }
}