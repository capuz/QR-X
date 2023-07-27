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
    public class ContenidoPrincipalController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/ContenidoPrincipal
        public IQueryable<Contenido_Principal> GetContenido_Principal()
        {
            try
            {

                return db.Contenido_Principal;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/ContenidoPrincipal/5
        [ResponseType(typeof(Contenido_Principal))]
        public async Task<IHttpActionResult> GetContenido_Principal(decimal id)
        {
            try
            {

                Contenido_Principal contenido_Principal = await db.Contenido_Principal.FindAsync(id);
                if (contenido_Principal == null)
                {
                    return NotFound();
                }

                return Ok(contenido_Principal);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/ContenidoPrincipal/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContenido_Principal(decimal id, string usuario, Contenido_Principal contenido_Principal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contenido_Principal.IdContenidoPrincipal)
            {
                return BadRequest();
            }

            db.Entry(contenido_Principal).State = EntityState.Modified;

            try
            {

                //Auditoria
                Contenido_Principal obj = db.Contenido_Principal.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "ContenidoPrincipal", 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Contenido_PrincipalExists(id))
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

        // POST: api/ContenidoPrincipal
        [ResponseType(typeof(Contenido_Principal))]
        public async Task<IHttpActionResult> PostContenido_Principal(string usuario, Contenido_Principal contenido_Principal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contenido_Principal.Add(contenido_Principal);
            await db.SaveChangesAsync();

            //Auditoria
            Contenido_Principal obj = db.Contenido_Principal.Find(contenido_Principal.IdContenidoPrincipal);
            Log.Auditoria(obj, int.Parse(contenido_Principal.IdContenidoPrincipal.ToString()), usuario, "ContenidoPrincipal", 1);
            //Auditoria

            //return CreatedAtRoute("DefaultApi", new { id = contenido_Principal.IdContenidoPrincipal }, contenido_Principal);
            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/ContenidoPrincipal/5
        [ResponseType(typeof(Contenido_Principal))]
        public async Task<IHttpActionResult> DeleteContenido_Principal(decimal id, string usuario)
        {

            try
            {

                Contenido_Principal contenido_Principal = await db.Contenido_Principal.FindAsync(id);
                if (contenido_Principal == null)
                {
                    return NotFound();
                }

                //Auditoria
                Contenido_Principal obj = db.Contenido_Principal.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "ContenidoPrincipal", 3);
                //Auditoria

                db.Contenido_Principal.Remove(contenido_Principal);
                await db.SaveChangesAsync();

                return Ok(contenido_Principal);

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

        private bool Contenido_PrincipalExists(decimal id)
        {
            return db.Contenido_Principal.Count(e => e.IdContenidoPrincipal == id) > 0;
        }
    }
}