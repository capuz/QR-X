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
    public class CONTENIDO_PRINCIPALController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/CONTENIDO_PRINCIPAL
        public IQueryable<CONTENIDO_PRINCIPAL> GetCONTENIDO_PRINCIPAL()
        {
            return db.CONTENIDO_PRINCIPAL;
        }

        // GET: api/CONTENIDO_PRINCIPAL/5
        [ResponseType(typeof(CONTENIDO_PRINCIPAL))]
        public async Task<IHttpActionResult> GetCONTENIDO_PRINCIPAL(decimal id)
        {
            CONTENIDO_PRINCIPAL cONTENIDO_PRINCIPAL = await db.CONTENIDO_PRINCIPAL.FindAsync(id);
            if (cONTENIDO_PRINCIPAL == null)
            {
                return NotFound();
            }

            return Ok(cONTENIDO_PRINCIPAL);
        }

        // PUT: api/CONTENIDO_PRINCIPAL/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCONTENIDO_PRINCIPAL(decimal id, CONTENIDO_PRINCIPAL cONTENIDO_PRINCIPAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cONTENIDO_PRINCIPAL.IdContenidoPrincipal)
            {
                return BadRequest();
            }

            db.Entry(cONTENIDO_PRINCIPAL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CONTENIDO_PRINCIPALExists(id))
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

        // POST: api/CONTENIDO_PRINCIPAL
        [ResponseType(typeof(CONTENIDO_PRINCIPAL))]
        public async Task<IHttpActionResult> PostCONTENIDO_PRINCIPAL(CONTENIDO_PRINCIPAL cONTENIDO_PRINCIPAL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CONTENIDO_PRINCIPAL.Add(cONTENIDO_PRINCIPAL);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cONTENIDO_PRINCIPAL.IdContenidoPrincipal }, cONTENIDO_PRINCIPAL);
        }

        // DELETE: api/CONTENIDO_PRINCIPAL/5
        [ResponseType(typeof(CONTENIDO_PRINCIPAL))]
        public async Task<IHttpActionResult> DeleteCONTENIDO_PRINCIPAL(decimal id)
        {
            CONTENIDO_PRINCIPAL cONTENIDO_PRINCIPAL = await db.CONTENIDO_PRINCIPAL.FindAsync(id);
            if (cONTENIDO_PRINCIPAL == null)
            {
                return NotFound();
            }

            db.CONTENIDO_PRINCIPAL.Remove(cONTENIDO_PRINCIPAL);
            await db.SaveChangesAsync();

            return Ok(cONTENIDO_PRINCIPAL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CONTENIDO_PRINCIPALExists(decimal id)
        {
            return db.CONTENIDO_PRINCIPAL.Count(e => e.IdContenidoPrincipal == id) > 0;
        }
    }
}