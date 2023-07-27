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
    public class PERMISOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/PERMISO
        public IQueryable<PERMISO> GetPERMISO()
        {
            return db.PERMISO;
        }

        // GET: api/PERMISO/5
        [ResponseType(typeof(PERMISO))]
        public async Task<IHttpActionResult> GetPERMISO(decimal id)
        {
            PERMISO pERMISO = await db.PERMISO.FindAsync(id);
            if (pERMISO == null)
            {
                return NotFound();
            }

            return Ok(pERMISO);
        }

        // PUT: api/PERMISO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPERMISO(decimal id, PERMISO pERMISO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pERMISO.IdPermiso)
            {
                return BadRequest();
            }

            db.Entry(pERMISO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PERMISOExists(id))
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

        // POST: api/PERMISO
        [ResponseType(typeof(PERMISO))]
        public async Task<IHttpActionResult> PostPERMISO(PERMISO pERMISO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PERMISO.Add(pERMISO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pERMISO.IdPermiso }, pERMISO);
        }

        // DELETE: api/PERMISO/5
        [ResponseType(typeof(PERMISO))]
        public async Task<IHttpActionResult> DeletePERMISO(decimal id)
        {
            PERMISO pERMISO = await db.PERMISO.FindAsync(id);
            if (pERMISO == null)
            {
                return NotFound();
            }

            db.PERMISO.Remove(pERMISO);
            await db.SaveChangesAsync();

            return Ok(pERMISO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PERMISOExists(decimal id)
        {
            return db.PERMISO.Count(e => e.IdPermiso == id) > 0;
        }
    }
}