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
    public class PERFILController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/PERFIL
        public IQueryable<PERFIL> GetPERFIL()
        {
            return db.PERFIL;
        }

        // GET: api/PERFIL/5
        [ResponseType(typeof(PERFIL))]
        public async Task<IHttpActionResult> GetPERFIL(decimal id)
        {
            PERFIL pERFIL = await db.PERFIL.FindAsync(id);
            if (pERFIL == null)
            {
                return NotFound();
            }

            return Ok(pERFIL);
        }

        // PUT: api/PERFIL/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPERFIL(decimal id, PERFIL pERFIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pERFIL.IdPerfil)
            {
                return BadRequest();
            }

            db.Entry(pERFIL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PERFILExists(id))
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

        // POST: api/PERFIL
        [ResponseType(typeof(PERFIL))]
        public async Task<IHttpActionResult> PostPERFIL(PERFIL pERFIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PERFIL.Add(pERFIL);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pERFIL.IdPerfil }, pERFIL);
        }

        // DELETE: api/PERFIL/5
        [ResponseType(typeof(PERFIL))]
        public async Task<IHttpActionResult> DeletePERFIL(decimal id)
        {
            PERFIL pERFIL = await db.PERFIL.FindAsync(id);
            if (pERFIL == null)
            {
                return NotFound();
            }

            db.PERFIL.Remove(pERFIL);
            await db.SaveChangesAsync();

            return Ok(pERFIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PERFILExists(decimal id)
        {
            return db.PERFIL.Count(e => e.IdPerfil == id) > 0;
        }
    }
}