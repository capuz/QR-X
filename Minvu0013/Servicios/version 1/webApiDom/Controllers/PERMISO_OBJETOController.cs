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
    public class PERMISO_OBJETOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/PERMISO_OBJETO
        public IQueryable<PERMISO_OBJETO> GetPERMISO_OBJETO()
        {
            return db.PERMISO_OBJETO;
        }

        // GET: api/PERMISO_OBJETO/5
        [ResponseType(typeof(PERMISO_OBJETO))]
        public async Task<IHttpActionResult> GetPERMISO_OBJETO(decimal id)
        {
            PERMISO_OBJETO pERMISO_OBJETO = await db.PERMISO_OBJETO.FindAsync(id);
            if (pERMISO_OBJETO == null)
            {
                return NotFound();
            }

            return Ok(pERMISO_OBJETO);
        }

        // PUT: api/PERMISO_OBJETO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPERMISO_OBJETO(decimal id, PERMISO_OBJETO pERMISO_OBJETO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pERMISO_OBJETO.IdPermisoObjeto)
            {
                return BadRequest();
            }

            db.Entry(pERMISO_OBJETO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PERMISO_OBJETOExists(id))
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

        // POST: api/PERMISO_OBJETO
        [ResponseType(typeof(PERMISO_OBJETO))]
        public async Task<IHttpActionResult> PostPERMISO_OBJETO(PERMISO_OBJETO pERMISO_OBJETO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PERMISO_OBJETO.Add(pERMISO_OBJETO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pERMISO_OBJETO.IdPermisoObjeto }, pERMISO_OBJETO);
        }

        // DELETE: api/PERMISO_OBJETO/5
        [ResponseType(typeof(PERMISO_OBJETO))]
        public async Task<IHttpActionResult> DeletePERMISO_OBJETO(decimal id)
        {
            PERMISO_OBJETO pERMISO_OBJETO = await db.PERMISO_OBJETO.FindAsync(id);
            if (pERMISO_OBJETO == null)
            {
                return NotFound();
            }

            db.PERMISO_OBJETO.Remove(pERMISO_OBJETO);
            await db.SaveChangesAsync();

            return Ok(pERMISO_OBJETO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PERMISO_OBJETOExists(decimal id)
        {
            return db.PERMISO_OBJETO.Count(e => e.IdPermisoObjeto == id) > 0;
        }
    }
}