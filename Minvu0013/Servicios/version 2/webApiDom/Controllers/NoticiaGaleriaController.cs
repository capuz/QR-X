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
    public class NoticiaGaleriaController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/NoticiaGaleria
        public IQueryable<Noticia_Galeria> GetNoticia_Galeria()
        {
            try
            {

                return db.Noticia_Galeria;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/NoticiaGaleria/5
        [ResponseType(typeof(Noticia_Galeria))]
        public async Task<IHttpActionResult> GetNoticia_Galeria(decimal id)
        {
            try
            {

                Noticia_Galeria noticia_Galeria = await db.Noticia_Galeria.FindAsync(id);
                if (noticia_Galeria == null)
                {
                    return NotFound();
                }

                return Ok(noticia_Galeria);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5,  "ContenidoLogo", MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/NoticiaGaleria/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNoticia_Galeria(decimal id, Noticia_Galeria noticia_Galeria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticia_Galeria.IdNoticiaGaleria)
            {
                return BadRequest();
            }

            db.Entry(noticia_Galeria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Noticia_GaleriaExists(id))
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

        // POST: api/NoticiaGaleria
        [ResponseType(typeof(Noticia_Galeria))]
        public async Task<IHttpActionResult> PostNoticia_Galeria(Noticia_Galeria noticia_Galeria)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Noticia_Galeria.Add(noticia_Galeria);
                await db.SaveChangesAsync();

                //return CreatedAtRoute("DefaultApi", new { id = noticia_Galeria.IdNoticiaGaleria }, noticia_Galeria);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/NoticiaGaleria/5
        [ResponseType(typeof(Noticia_Galeria))]
        public async Task<IHttpActionResult> DeleteNoticia_Galeria(decimal id)
        {
            try
            {

                Noticia_Galeria noticia_Galeria = await db.Noticia_Galeria.FindAsync(id);
                if (noticia_Galeria == null)
                {
                    return NotFound();
                }

                db.Noticia_Galeria.Remove(noticia_Galeria);
                await db.SaveChangesAsync();

                return Ok(noticia_Galeria);

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

        private bool Noticia_GaleriaExists(decimal id)
        {
            return db.Noticia_Galeria.Count(e => e.IdNoticiaGaleria == id) > 0;
        }
    }
}