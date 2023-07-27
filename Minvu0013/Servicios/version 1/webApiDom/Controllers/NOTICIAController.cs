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
    public class NOTICIAController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/NOTICIA
        public IQueryable<NOTICIA> GetNOTICIA()
        {
            return db.NOTICIA;
        }

        // GET: api/NOTICIA/5
        [ResponseType(typeof(NOTICIA))]
        public async Task<IHttpActionResult> GetNOTICIA(decimal id)
        {
            NOTICIA nOTICIA = await db.NOTICIA.FindAsync(id);
            if (nOTICIA == null)
            {
                return NotFound();
            }

            return Ok(nOTICIA);
        }

        // GET: api/NOTICIA/5
        [ResponseType(typeof(NOTICIA))]
        public List<NOTICIA> GetNOTICIA_HomePage()
        {
            List<NOTICIA> nOTICIA = db.NOTICIA.Where(k => k.Destacada == 1 && k.Activo == 1).ToList();

            if (nOTICIA.Count == 0)
            {
                return null;
            }

            return nOTICIA;
        }

        [ResponseType(typeof(NOTICIA))]
        public List<NOTICIA> GetNOTICIA_Historica()
        {
            List<NOTICIA> nOTICIA = db.NOTICIA.Where(k => k.Destacada == 0 && k.Activo == 1).ToList();

            if (nOTICIA.Count == 0)
            {
                return null;
            }

            return nOTICIA;
        }

        // PUT: api/NOTICIA/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNOTICIA(decimal id, NOTICIA nOTICIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nOTICIA.IdNoticia)
            {
                return BadRequest();
            }

            db.Entry(nOTICIA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NOTICIAExists(id))
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

        // POST: api/NOTICIA
        [ResponseType(typeof(NOTICIA))]
        public async Task<IHttpActionResult> PostNOTICIA(NOTICIA nOTICIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try {
                db.NOTICIA.Add(nOTICIA);
                await db.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                var x = 1;
            }
                
            return CreatedAtRoute("DefaultApi", new { id = nOTICIA.IdNoticia }, nOTICIA);
        }

        // DELETE: api/NOTICIA/5
        [ResponseType(typeof(NOTICIA))]
        public async Task<IHttpActionResult> DeleteNOTICIA(decimal id)
        {
            NOTICIA nOTICIA = await db.NOTICIA.FindAsync(id);
            if (nOTICIA == null)
            {
                return NotFound();
            }

            nOTICIA.Activo = 0;
            await db.SaveChangesAsync();

            return Ok(nOTICIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NOTICIAExists(decimal id)
        {
            return db.NOTICIA.Count(e => e.IdNoticia == id) > 0;
        }
    }
}