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
using webApiDom;
using System.Reflection;

namespace webApiDom.Controllers
{
    public class ContenidoLogoController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/ContenidoLogo
        public IQueryable<Contenido_Logo> GetContenido_Logo()
        {
            try
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), "GetContenido_Logo");
                return db.Contenido_Logo;
            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/ContenidoLogo/5
        [ResponseType(typeof(Contenido_Logo))]
        public async Task<IHttpActionResult> GetContenido_Logo(decimal id)
        {
            try
            {

                Contenido_Logo contenido_Logo = await db.Contenido_Logo.FindAsync(id);
                if (contenido_Logo == null)
                {
                    Log.Log(3, 5, Log.GetCurrentPageName(), "Contenido Logo no encontrado : " + id.ToString() + "");
                    return NotFound();
                }

                return Ok(contenido_Logo);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }

        }

        
        // PUT: api/ContenidoLogo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContenido_Logo(decimal id, string usuario, Contenido_Logo contenido_Logo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contenido_Logo.IdContenidoLogo)
            {
                return BadRequest();
            }

            db.Entry(contenido_Logo).State = EntityState.Modified;

            try
            {

                //Auditoria
                Contenido_Logo obj = db.Contenido_Logo.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, Log.GetCurrentPageName(), 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Contenido_LogoExists(id))
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

        // POST: api/ContenidoLogo
        [ResponseType(typeof(Contenido_Logo))]
        public async Task<IHttpActionResult> PostContenido_Logo(string usuario, Contenido_Logo contenido_Logo)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Contenido_Logo.Add(contenido_Logo);
                await db.SaveChangesAsync();

                //Auditoria
                Contenido_Logo obj = db.Contenido_Logo.Find(contenido_Logo.IdContenidoLogo);
                Log.Auditoria(obj, int.Parse(contenido_Logo.IdContenidoLogo.ToString()), usuario, Log.GetCurrentPageName(), 1);
                //Auditoria

                //return CreatedAtRoute("DefaultApi", new { id = contenido_Logo.IdContenidoLogo }, contenido_Logo);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/ContenidoLogo/5
        [ResponseType(typeof(Contenido_Logo))]
        public async Task<IHttpActionResult> DeleteContenido_Logo(decimal id, string usuario)
        {
            try
            {

                Contenido_Logo contenido_Logo = await db.Contenido_Logo.FindAsync(id);
                if (contenido_Logo == null)
                {
                    return NotFound();
                }

                //Auditoria
                Contenido_Logo obj = db.Contenido_Logo.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, Log.GetCurrentPageName(), 3);
                //Auditoria

                db.Contenido_Logo.Remove(contenido_Logo);
                await db.SaveChangesAsync();

                return Ok(contenido_Logo);

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

        private bool Contenido_LogoExists(decimal id)
        {
            return db.Contenido_Logo.Count(e => e.IdContenidoLogo == id) > 0;
        }

    }
}