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
    public class COMUNAController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/COMUNA
        public IQueryable<COMUNA> GetCOMUNA()
        {
            return db.COMUNA;
        }

        // GET: api/COMUNA/5
        [ResponseType(typeof(COMUNA))]
        public async Task<IHttpActionResult> GetCOMUNA(decimal id)
        {
            COMUNA cOMUNA = await db.COMUNA.FindAsync(id);
            if (cOMUNA == null)
            {
                return NotFound();
            }

            return Ok(cOMUNA);
        }


        [ResponseType(typeof(COMUNA))]
        public List<COMUNA> GetRegion(decimal id)
        {

            List<COMUNA> cOMUNA = db.COMUNA.Where(k => k.IdRegion == id).ToList();
            
            if (cOMUNA == null)
            {
                return null;
            }

            return cOMUNA;
        }

        // PUT: api/COMUNA/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCOMUNA(decimal id, COMUNA cOMUNA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cOMUNA.IdComuna)
            {
                return BadRequest();
            }

            db.Entry(cOMUNA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!COMUNAExists(id))
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

        // POST: api/COMUNA
        [ResponseType(typeof(COMUNA))]
        public async Task<IHttpActionResult> PostCOMUNA(COMUNA cOMUNA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.COMUNA.Add(cOMUNA);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cOMUNA.IdComuna }, cOMUNA);
        }

        // DELETE: api/COMUNA/5
        [ResponseType(typeof(COMUNA))]
        public async Task<IHttpActionResult> DeleteCOMUNA(decimal id)
        {
            COMUNA cOMUNA = await db.COMUNA.FindAsync(id);
            if (cOMUNA == null)
            {
                return NotFound();
            }

            db.COMUNA.Remove(cOMUNA);
            await db.SaveChangesAsync();

            return Ok(cOMUNA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool COMUNAExists(decimal id)
        {
            return db.COMUNA.Count(e => e.IdComuna == id) > 0;
        }
    }
}