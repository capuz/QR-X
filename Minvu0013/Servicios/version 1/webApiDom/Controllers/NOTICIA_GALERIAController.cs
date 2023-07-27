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
    public class NOTICIA_GALERIAController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/NOTICIA_GALERIA
        public IQueryable<NOTICIA_GALERIA> GetNOTICIA_GALERIA()
        {
            return db.NOTICIA_GALERIA;
        }

        // GET: api/NOTICIA_GALERIA/5
        [ResponseType(typeof(NOTICIA_GALERIA))]
        public async Task<IHttpActionResult> GetNOTICIA_GALERIA(decimal id)
        {
            NOTICIA_GALERIA nOTICIA_GALERIA = await db.NOTICIA_GALERIA.FindAsync(id);
            if (nOTICIA_GALERIA == null)
            {
                return NotFound();
            }

            return Ok(nOTICIA_GALERIA);
        }

        // PUT: api/NOTICIA_GALERIA/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNOTICIA_GALERIA(decimal id, NOTICIA_GALERIA nOTICIA_GALERIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nOTICIA_GALERIA.IdNoticiaGaleria)
            {
                return BadRequest();
            }

            db.Entry(nOTICIA_GALERIA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NOTICIA_GALERIAExists(id))
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

        // POST: api/NOTICIA_GALERIA
        [ResponseType(typeof(NOTICIA_GALERIA))]
        public async Task<IHttpActionResult> PostNOTICIA_GALERIA(NOTICIA_GALERIA nOTICIA_GALERIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NOTICIA_GALERIA.Add(nOTICIA_GALERIA);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nOTICIA_GALERIA.IdNoticiaGaleria }, nOTICIA_GALERIA);
        }

        // DELETE: api/NOTICIA_GALERIA/5
        [ResponseType(typeof(NOTICIA_GALERIA))]
        public async Task<IHttpActionResult> DeleteNOTICIA_GALERIA(decimal id)
        {
            NOTICIA_GALERIA nOTICIA_GALERIA = await db.NOTICIA_GALERIA.FindAsync(id);
            if (nOTICIA_GALERIA == null)
            {
                return NotFound();
            }

            db.NOTICIA_GALERIA.Remove(nOTICIA_GALERIA);
            await db.SaveChangesAsync();

            return Ok(nOTICIA_GALERIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NOTICIA_GALERIAExists(decimal id)
        {
            return db.NOTICIA_GALERIA.Count(e => e.IdNoticiaGaleria == id) > 0;
        }
    }
}