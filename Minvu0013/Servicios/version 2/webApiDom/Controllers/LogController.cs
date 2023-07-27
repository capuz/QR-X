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
    public class LogController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/Log
        public IQueryable<Log> GetLog()
        {
            return db.Log;
        }

        // GET: api/Log/5
        [ResponseType(typeof(Log))]
        public async Task<IHttpActionResult> GetLog(decimal id)
        {
            Log log = await db.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // PUT: api/Log/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLog(decimal id, Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != log.IdLog)
            {
                return BadRequest();
            }

            db.Entry(log).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
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

        // POST: api/Log
        [ResponseType(typeof(Log))]
        public async Task<IHttpActionResult> PostLog(Log log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Log.Add(log);
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = log.IdLog }, log);
            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/Log/5
        [ResponseType(typeof(Log))]
        public async Task<IHttpActionResult> DeleteLog(decimal id)
        {
            Log log = await db.Log.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            db.Log.Remove(log);
            await db.SaveChangesAsync();

            return Ok(log);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogExists(decimal id)
        {
            return db.Log.Count(e => e.IdLog == id) > 0;
        }
    }
}