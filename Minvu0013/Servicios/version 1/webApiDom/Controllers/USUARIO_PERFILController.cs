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
    public class USUARIO_PERFILController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/USUARIO_PERFIL
        public IQueryable<USUARIO_PERFIL> GetUSUARIO_PERFIL()
        {
            return db.USUARIO_PERFIL;
        }

        // GET: api/USUARIO_PERFIL/5
        [ResponseType(typeof(USUARIO_PERFIL))]
        public async Task<IHttpActionResult> GetUSUARIO_PERFIL(decimal id)
        {
            USUARIO_PERFIL uSUARIO_PERFIL = await db.USUARIO_PERFIL.FindAsync(id);
            if (uSUARIO_PERFIL == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO_PERFIL);
        }

        // PUT: api/USUARIO_PERFIL/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSUARIO_PERFIL(decimal id, USUARIO_PERFIL uSUARIO_PERFIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO_PERFIL.IdUsuarioPerfil)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO_PERFIL).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIO_PERFILExists(id))
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

        // POST: api/USUARIO_PERFIL
        [ResponseType(typeof(USUARIO_PERFIL))]
        public async Task<IHttpActionResult> PostUSUARIO_PERFIL(USUARIO_PERFIL uSUARIO_PERFIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO_PERFIL.Add(uSUARIO_PERFIL);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO_PERFIL.IdUsuarioPerfil }, uSUARIO_PERFIL);
        }

        // DELETE: api/USUARIO_PERFIL/5
        [ResponseType(typeof(USUARIO_PERFIL))]
        public async Task<IHttpActionResult> DeleteUSUARIO_PERFIL(decimal id)
        {
            USUARIO_PERFIL uSUARIO_PERFIL = await db.USUARIO_PERFIL.FindAsync(id);
            if (uSUARIO_PERFIL == null)
            {
                return NotFound();
            }

            db.USUARIO_PERFIL.Remove(uSUARIO_PERFIL);
            await db.SaveChangesAsync();

            return Ok(uSUARIO_PERFIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIO_PERFILExists(decimal id)
        {
            return db.USUARIO_PERFIL.Count(e => e.IdUsuarioPerfil == id) > 0;
        }
    }
}