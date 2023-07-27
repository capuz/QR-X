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
    public class AUDITORIAController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/AUDITORIA
        public IQueryable<AUDITORIA> GetAuditoria()
        {
            return db.AUDITORIA;
        }

        // GET: api/AUDITORIA/5
        [ResponseType(typeof(AUDITORIA))]
        public async Task<IHttpActionResult> GetAuditoria(decimal id)
        {
            AUDITORIA aUDITORIA = await db.AUDITORIA.FindAsync(id);
            if (aUDITORIA == null)
            {
                return NotFound();
            }

            return Ok(aUDITORIA);
        }

        // GET: api/student/5
        public IEnumerable<USP_AUDITORIA_Select_Filtro_Result> GetFiltro(string param1, string param2, string param3)
        {
            return db.USP_AUDITORIA_Select_Filtro(param1, param2, param3).AsEnumerable();
        }

        // PUT: api/AUDITORIA/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAUDITORIA(decimal id, AUDITORIA aUDITORIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aUDITORIA.IdAuditoria)
            {
                return BadRequest();
            }

            db.Entry(aUDITORIA).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AUDITORIAExists(id))
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

        // POST: api/AUDITORIA
        [ResponseType(typeof(AUDITORIA))]
        public async Task<IHttpActionResult> PostAUDITORIA(AUDITORIA aUDITORIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AUDITORIA.Add(aUDITORIA);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = aUDITORIA.IdAuditoria }, aUDITORIA);
        }

        // DELETE: api/AUDITORIA/5
        [ResponseType(typeof(AUDITORIA))]
        public async Task<IHttpActionResult> DeleteAUDITORIA(decimal id)
        {
            AUDITORIA aUDITORIA = await db.AUDITORIA.FindAsync(id);
            if (aUDITORIA == null)
            {
                return NotFound();
            }

            db.AUDITORIA.Remove(aUDITORIA);
            await db.SaveChangesAsync();

            return Ok(aUDITORIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AUDITORIAExists(decimal id)
        {
            return db.AUDITORIA.Count(e => e.IdAuditoria == id) > 0;
        }
    }
}