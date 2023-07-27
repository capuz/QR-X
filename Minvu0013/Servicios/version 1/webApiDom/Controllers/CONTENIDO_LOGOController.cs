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
    public class CONTENIDO_LOGOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/CONTENIDO_LOGO
        public IQueryable<CONTENIDO_LOGO> GetCONTENIDO_LOGO()
        {
            return db.CONTENIDO_LOGO;
        }

        // GET: api/CONTENIDO_LOGO/5
        [ResponseType(typeof(CONTENIDO_LOGO))]
        public async Task<IHttpActionResult> GetCONTENIDO_LOGO(decimal id)
        {
            CONTENIDO_LOGO cONTENIDO_LOGO = await db.CONTENIDO_LOGO.FindAsync(id);
            if (cONTENIDO_LOGO == null)
            {
                return NotFound();
            }

            return Ok(cONTENIDO_LOGO);
        }

        // PUT: api/CONTENIDO_LOGO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCONTENIDO_LOGO(decimal id, CONTENIDO_LOGO cONTENIDO_LOGO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cONTENIDO_LOGO.IdContenidoLogo)
            {
                return BadRequest();
            }

            db.Entry(cONTENIDO_LOGO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CONTENIDO_LOGOExists(id))
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

        // POST: api/CONTENIDO_LOGO
        [ResponseType(typeof(CONTENIDO_LOGO))]
        public async Task<IHttpActionResult> PostCONTENIDO_LOGO(CONTENIDO_LOGO cONTENIDO_LOGO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CONTENIDO_LOGO.Add(cONTENIDO_LOGO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cONTENIDO_LOGO.IdContenidoLogo }, cONTENIDO_LOGO);
        }

        // DELETE: api/CONTENIDO_LOGO/5
        [ResponseType(typeof(CONTENIDO_LOGO))]
        public async Task<IHttpActionResult> DeleteCONTENIDO_LOGO(decimal id)
        {
            CONTENIDO_LOGO cONTENIDO_LOGO = await db.CONTENIDO_LOGO.FindAsync(id);
            if (cONTENIDO_LOGO == null)
            {
                return NotFound();
            }

            db.CONTENIDO_LOGO.Remove(cONTENIDO_LOGO);
            await db.SaveChangesAsync();

            return Ok(cONTENIDO_LOGO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CONTENIDO_LOGOExists(decimal id)
        {
            return db.CONTENIDO_LOGO.Count(e => e.IdContenidoLogo == id) > 0;
        }
    }
}