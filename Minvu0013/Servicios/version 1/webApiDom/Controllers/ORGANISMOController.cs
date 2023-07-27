using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using webApiDom.Models;

namespace webApiDom.Controllers
{
    public class ORGANISMOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/ORGANISMO
        public IQueryable<ORGANISMO> GetORGANISMO()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ORGANISMO;
        }

        // GET: api/ORGANISMO/5
        [ResponseType(typeof(ORGANISMO))]
        public async Task<IHttpActionResult> GetORGANISMO(decimal id)
        {
            ORGANISMO oRGANISMO = await db.ORGANISMO.FindAsync(id);
            if (oRGANISMO == null)
            {
                return NotFound();
            }

            return Ok(oRGANISMO);

        }

        public IEnumerable<USP_ORGANISMO_Select_Filtro_Result> GetFiltro(string Param1)
        {
            return db.USP_ORGANISMO_Select_Filtro(Param1).AsEnumerable();
        }

        // PUT: api/ORGANISMO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutORGANISMO(decimal id, ORGANISMO oRGANISMO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oRGANISMO.IdOrganismo)
            {
                return BadRequest();
            }

            db.Entry(oRGANISMO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ORGANISMOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ORGANISMO
        [ResponseType(typeof(ORGANISMO))]
        public async Task<IHttpActionResult> PostORGANISMO(ORGANISMO oRGANISMO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ORGANISMO.Add(oRGANISMO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = oRGANISMO.IdOrganismo }, oRGANISMO);
        }

        // DELETE: api/ORGANISMO/5
        [ResponseType(typeof(ORGANISMO))]
        public async Task<IHttpActionResult> DeleteORGANISMO(decimal id)
        {
            ORGANISMO oRGANISMO = await db.ORGANISMO.FindAsync(id);
            if (oRGANISMO == null)
            {
                return NotFound();
            }

            oRGANISMO.Activo = 0;
            await db.SaveChangesAsync();

            return Ok(oRGANISMO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ORGANISMOExists(decimal id)
        {
            return db.ORGANISMO.Count(e => e.IdOrganismo == id) > 0;
        }
    }
}