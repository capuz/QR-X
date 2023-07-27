using System.Linq;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.Infrastructure.DataAccessLayer
{
    public class AmbientesRepository
    {
        private readonly LaboratorioEntities _db;
        public AmbientesRepository(LaboratorioEntities dbContext)
        {
            this._db = dbContext;
        }

        public string ObtenerNombreAmbiente(string nombreServidor, string nombreBaseDatos)
        {
            var ambiente = _db.Ambientes.FirstOrDefault(a => a.NombreServidor == nombreServidor && a.NombreBaseDatos == nombreBaseDatos);
            return ambiente == null ? "Ambiente no registrado: " + nombreBaseDatos : ambiente.Nombre;
        }
    }
}