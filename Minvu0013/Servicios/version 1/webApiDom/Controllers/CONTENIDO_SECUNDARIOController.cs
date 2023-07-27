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
    public class CONTENIDO_SECUNDARIOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/CONTENIDO_SECUNDARIO
        public IQueryable<CONTENIDO_SECUNDARIO> GetCONTENIDO_SECUNDARIO()
        {
            return db.CONTENIDO_SECUNDARIO;
        }

        // GET: api/CONTENIDO_SECUNDARIO/5
        [ResponseType(typeof(CONTENIDO_SECUNDARIO))]
        public async Task<IHttpActionResult> GetCONTENIDO_SECUNDARIO(decimal id)
        {
            CONTENIDO_SECUNDARIO cONTENIDO_SECUNDARIO = await db.CONTENIDO_SECUNDARIO.FindAsync(id);
            if (cONTENIDO_SECUNDARIO == null)
            {
                return NotFound();
            }

            return Ok(cONTENIDO_SECUNDARIO);
        }

        // PUT: api/CONTENIDO_SECUNDARIO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCONTENIDO_SECUNDARIO(decimal id, CONTENIDO_SECUNDARIO cONTENIDO_SECUNDARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cONTENIDO_SECUNDARIO.IdContenidoSecundario)
            {
                return BadRequest();
            }

            db.Entry(cONTENIDO_SECUNDARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CONTENIDO_SECUNDARIOExists(id))
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

        // POST: api/CONTENIDO_SECUNDARIO
        [ResponseType(typeof(CONTENIDO_SECUNDARIO))]
        public async Task<IHttpActionResult> PostCONTENIDO_SECUNDARIO(CONTENIDO_SECUNDARIO cONTENIDO_SECUNDARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CONTENIDO_SECUNDARIO.Add(cONTENIDO_SECUNDARIO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cONTENIDO_SECUNDARIO.IdContenidoSecundario }, cONTENIDO_SECUNDARIO);
        }

        // DELETE: api/CONTENIDO_SECUNDARIO/5
        [ResponseType(typeof(CONTENIDO_SECUNDARIO))]
        public async Task<IHttpActionResult> DeleteCONTENIDO_SECUNDARIO(decimal id)
        {
            CONTENIDO_SECUNDARIO cONTENIDO_SECUNDARIO = await db.CONTENIDO_SECUNDARIO.FindAsync(id);
            if (cONTENIDO_SECUNDARIO == null)
            {
                return NotFound();
            }

            db.CONTENIDO_SECUNDARIO.Remove(cONTENIDO_SECUNDARIO);
            await db.SaveChangesAsync();

            return Ok(cONTENIDO_SECUNDARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CONTENIDO_SECUNDARIOExists(decimal id)
        {
            return db.CONTENIDO_SECUNDARIO.Count(e => e.IdContenidoSecundario == id) > 0;
        }
    }
}