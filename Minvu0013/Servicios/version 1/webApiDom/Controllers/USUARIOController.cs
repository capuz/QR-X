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
    public class USUARIOController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/USUARIO
        public IQueryable<USUARIO> GetUsuario()
        {
            return db.USUARIO;
        }

        // GET: api/USUARIO/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> GetUsuario(decimal id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        [ResponseType(typeof(USUARIO))]
        public List<USUARIO> GetUsuario_Autocompletar(string nombre)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<USUARIO> uSUARIO = db.USUARIO.Where(k => k.Nombre.Contains(nombre)).ToList();

            if (uSUARIO.Count == 0)
            {
                return null;
            }

            return uSUARIO;
        }

        // GET: api/USUARIO_FILTRO/5
        public IEnumerable<USP_USUARIO_Select_Filtro_Result> GetFiltro(string param1, string param2, string param3, string param4, string param5)
        {
            return db.USP_USUARIO_Select_Filtro(param1, param2, param3, param4, param5).AsEnumerable();
        }

        // PUT: api/USUARIO/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUSUARIO(decimal id, USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uSUARIO.IdUsuario)
            {
                return BadRequest();
            }

            db.Entry(uSUARIO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
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

        // POST: api/USUARIO
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> PostUSUARIO(USUARIO uSUARIO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.USUARIO.Add(uSUARIO);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.IdUsuario }, uSUARIO);
        }

        // DELETE: api/USUARIO/5
        [ResponseType(typeof(USUARIO))]
        public async Task<IHttpActionResult> DeleteUSUARIO(decimal id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            db.USUARIO.Remove(uSUARIO);
            await db.SaveChangesAsync();

            return Ok(uSUARIO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(decimal id)
        {
            return db.USUARIO.Count(e => e.IdUsuario == id) > 0;
        }
    }
}