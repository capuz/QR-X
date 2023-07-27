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
    public class REGIONController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/REGION
        public IQueryable<REGION> GetREGION()
        {
            return db.REGION;
        }

        // GET: api/REGION/5
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> GetREGION(decimal id)
        {
            REGION rEGION = await db.REGION.FindAsync(id);
            if (rEGION == null)
            {
                return NotFound();
            }

            return Ok(rEGION);
        }

        // PUT: api/REGION/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutREGION(decimal id, REGION rEGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rEGION.IdRegion)
            {
                return BadRequest();
            }

            db.Entry(rEGION).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!REGIONExists(id))
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

        // POST: api/REGION
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> PostREGION(REGION rEGION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.REGION.Add(rEGION);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rEGION.IdRegion }, rEGION);
        }

        // DELETE: api/REGION/5
        [ResponseType(typeof(REGION))]
        public async Task<IHttpActionResult> DeleteREGION(decimal id)
        {
            REGION rEGION = await db.REGION.FindAsync(id);
            if (rEGION == null)
            {
                return NotFound();
            }

            db.REGION.Remove(rEGION);
            await db.SaveChangesAsync();

            return Ok(rEGION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool REGIONExists(decimal id)
        {
            return db.REGION.Count(e => e.IdRegion == id) > 0;
        }
    }
}