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
    public class ORGANISMO_TIPOController : ApiController
    {
        private domEntities db = new domEntities();

        public IQueryable<ORGANISMO_TIPO> GetORGANISMO_TIPO()
        {
            return db.ORGANISMO_TIPO;
        }

        // GET: api/ORGANISMO_TIPO/5
        [ResponseType(typeof(ORGANISMO_TIPO))]
        public async Task<IHttpActionResult> GetORGANISMO_TIPO(decimal id)
        {
            ORGANISMO_TIPO oRGANISMO_TIPO = await db.ORGANISMO_TIPO.FindAsync(id);
            if (oRGANISMO_TIPO == null)
            {
                return NotFound();
            }

            return Ok(oRGANISMO_TIPO);

        }

        // PUT: api/ORGANISMO_TIPO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutORGANISMO_TIPO(decimal id, ORGANISMO_TIPO oRGANISMO_TIPO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oRGANISMO_TIPO.IdOrganismoTipo)
            {
                return BadRequest();
            }

            db.Entry(oRGANISMO_TIPO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ORGANISMO_TIPOExists(id))
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

        // POST: api/ORGANISMO_TIPO
        [ResponseType(typeof(ORGANISMO_TIPO))]
        public async Task<IHttpActionResult> PostORGANISMO_TIPO(ORGANISMO_TIPO oRGANISMO_TIPO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ORGANISMO_TIPO.Add(oRGANISMO_TIPO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = oRGANISMO_TIPO.IdOrganismoTipo }, oRGANISMO_TIPO);
        }

        // DELETE: api/ORGANISMO_TIPO/5
        [ResponseType(typeof(ORGANISMO_TIPO))]
        public async Task<IHttpActionResult> DeleteORGANISMO_TIPO(decimal id)
        {
            ORGANISMO_TIPO oRGANISMO_TIPO = await db.ORGANISMO_TIPO.FindAsync(id);
            if (oRGANISMO_TIPO == null)
            {
                return NotFound();
            }

            db.ORGANISMO_TIPO.Remove(oRGANISMO_TIPO);
            await db.SaveChangesAsync();

            return Ok(oRGANISMO_TIPO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ORGANISMO_TIPOExists(decimal id)
        {
            return db.ORGANISMO_TIPO.Count(e => e.IdOrganismoTipo == id) > 0;
        }
    }
}