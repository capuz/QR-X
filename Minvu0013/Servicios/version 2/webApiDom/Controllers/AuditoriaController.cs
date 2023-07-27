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
using System.Reflection;

namespace webApiDom.Controllers
{
    public class AuditoriaController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/Auditoria
        public IQueryable<Auditoria> GetAuditoria()
        {
            try
            {
                return db.Auditoria;
            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/Auditoria/5
        [ResponseType(typeof(Auditoria))]
        public async Task<IHttpActionResult> GetAuditoria(decimal id)
        {

            try
            {

                Auditoria auditoria = await db.Auditoria.FindAsync(id);
                if (auditoria == null)
                {
                    return NotFound();
                }

                return Ok(auditoria);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Auditoria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuditoria(decimal id, Auditoria auditoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditoria.IdAuditoria)
            {
                return BadRequest();
            }

            db.Entry(auditoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Auditoria
        [ResponseType(typeof(Auditoria))]
        public async Task<IHttpActionResult> PostAuditoria(Auditoria auditoria)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Auditoria.Add(auditoria);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuditoriaExists(auditoria.IdAuditoria))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = auditoria.IdAuditoria }, auditoria);
            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Auditoria/5
        [ResponseType(typeof(Auditoria))]
        public async Task<IHttpActionResult> DeleteAuditoria(decimal id)
        {
            try
            {

                Auditoria auditoria = await db.Auditoria.FindAsync(id);
                if (auditoria == null)
                {
                    return NotFound();
                }

                db.Auditoria.Remove(auditoria);
                await db.SaveChangesAsync();

                return Ok(auditoria);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditoriaExists(decimal id)
        {
            return db.Auditoria.Count(e => e.IdAuditoria == id) > 0;
        }
    }
}