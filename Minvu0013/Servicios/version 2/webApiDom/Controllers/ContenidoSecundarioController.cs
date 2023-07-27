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
    public class ContenidoSecundarioController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/ContenidoSecundario
        public IQueryable<Contenido_Secundario> GetContenido_Secundario()
        {
            try
            {

                return db.Contenido_Secundario;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/ContenidoSecundario/5
        [ResponseType(typeof(Contenido_Secundario))]
        public async Task<IHttpActionResult> GetContenido_Secundario(decimal id)
        {
            try
            {

                Contenido_Secundario contenido_Secundario = await db.Contenido_Secundario.FindAsync(id);
                if (contenido_Secundario == null)
                {
                    return NotFound();
                }

                return Ok(contenido_Secundario);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/ContenidoSecundario/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContenido_Secundario(decimal id, string usuario, Contenido_Secundario contenido_Secundario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contenido_Secundario.IdContenidoSecundario)
            {
                return BadRequest();
            }

            db.Entry(contenido_Secundario).State = EntityState.Modified;

            try
            {
                //Auditoria
                Contenido_Secundario obj = db.Contenido_Secundario.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "ContenidoSecundario", 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Contenido_SecundarioExists(id))
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

        // POST: api/ContenidoSecundario
        [ResponseType(typeof(Contenido_Secundario))]
        public async Task<IHttpActionResult> PostContenido_Secundario(string usuario, Contenido_Secundario contenido_Secundario)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Contenido_Secundario.Add(contenido_Secundario);
                await db.SaveChangesAsync();

                //Auditoria
                Contenido_Secundario obj = db.Contenido_Secundario.Find(contenido_Secundario.IdContenidoSecundario);
                Log.Auditoria(obj, int.Parse(contenido_Secundario.IdContenidoSecundario.ToString()), usuario, "ContenidoSecundario", 1);
                //Auditoria

                //return CreatedAtRoute("DefaultApi", new { id = contenido_Secundario.IdContenidoSecundario }, contenido_Secundario);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/ContenidoSecundario/5
        [ResponseType(typeof(Contenido_Secundario))]
        public async Task<IHttpActionResult> DeleteContenido_Secundario(decimal id, string usuario)
        {
            try
            {

                Contenido_Secundario contenido_Secundario = await db.Contenido_Secundario.FindAsync(id);
                if (contenido_Secundario == null)
                {
                    return NotFound();
                }

                //Auditoria
                Contenido_Secundario obj = db.Contenido_Secundario.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "ContenidoSecundario", 3);
                //Auditoria

                db.Contenido_Secundario.Remove(contenido_Secundario);
                await db.SaveChangesAsync();

                return Ok(contenido_Secundario);

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

        private bool Contenido_SecundarioExists(decimal id)
        {
            return db.Contenido_Secundario.Count(e => e.IdContenidoSecundario == id) > 0;
        }
    }
}