using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
    public class NoticiaController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/Noticia
        public IQueryable<Noticia> GetNoticia()
        {
            try
            {

                return db.Noticia;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/Noticia/5
        [ResponseType(typeof(Noticia))]
        public async Task<IHttpActionResult> GetNoticia(decimal id)
        {
            try
            {

                Noticia noticia = await db.Noticia.FindAsync(id);
                if (noticia == null)
                {
                    return NotFound();
                }

                return Ok(noticia);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [ResponseType(typeof(Noticia))]
        public List<Noticia> GetNoticia_HomePage()
        {
            try
            {

                List<Noticia> nOTICIA = db.Noticia.Where(k => k.Destacada == true && k.Activo == true).ToList();

                if (nOTICIA.Count == 0)
                {
                    return null;
                }

                return nOTICIA;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        [ResponseType(typeof(Noticia))]
        public List<Noticia> GetNoticia_Historica()
        {
            try
            {
                List<Noticia> nOTICIA = db.Noticia.Where(k => k.Destacada == false && k.Activo == true).ToList();

                if (nOTICIA.Count == 0)
                {
                    return null;
                }

                return nOTICIA;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }


        [ResponseType(typeof(Noticia))]
        public async Task<IHttpActionResult> PatchNoticia_Publicacion(decimal id, string usuario)
        {
            try
            {

                Noticia noticia = await db.Noticia.FindAsync(id);
                if (noticia == null)
                {
                    return NotFound();
                }

                //Auditoria
                Noticia obj = db.Noticia.Find(noticia.IdNoticia);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Noticia", 3);
                //Auditoria

                noticia.FechaPublicacion = DateTime.Now;
                await db.SaveChangesAsync();

                return Ok(noticia);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/Noticia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNoticia(decimal id, string usuario, Noticia noticia)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticia.IdNoticia)
            {
                return BadRequest();
            }

            db.Entry(noticia).State = EntityState.Modified;

            try
            {
                //Auditoria
                Noticia obj = db.Noticia.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Noticia", 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiaExists(id))
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

        // POST: api/Noticia
        [ResponseType(typeof(Noticia))]
        public async Task<IHttpActionResult> PostNoticia(string usuario, Noticia noticia)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Noticia.Add(noticia);
                await db.SaveChangesAsync();

                //Auditoria
                Noticia obj = db.Noticia.Find(noticia.IdNoticia);
                Log.Auditoria(obj, int.Parse(noticia.IdNoticia.ToString()), usuario, "Noticia", 1);
                //Auditoria

                //return CreatedAtRoute("DefaultApi", new { id = noticia.IdNoticia }, noticia);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Noticia/5
        [ResponseType(typeof(Noticia))]
        public async Task<IHttpActionResult> DeleteNoticia(decimal id, string usuario)
        {
            try
            {

                Noticia noticia = await db.Noticia.FindAsync(id);
                if (noticia == null)
                {
                    return NotFound();
                }

                //Auditoria
                Noticia obj = db.Noticia.Find(noticia.IdNoticia);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Noticia", 3);
                //Auditoria

                noticia.Activo = false;
                await db.SaveChangesAsync();

                return Ok(noticia);

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

        private bool NoticiaExists(decimal id)
        {
            return db.Noticia.Count(e => e.IdNoticia == id) > 0;
        }
    }
}