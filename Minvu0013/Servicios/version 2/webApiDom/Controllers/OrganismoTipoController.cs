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
    public class OrganismoTipoController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/OrganismoTipo
        public IQueryable<Organismo_Tipo> GetOrganismo_Tipo()
        {
            try
            {

                return db.Organismo_Tipo;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/OrganismoTipo/5
        [ResponseType(typeof(Organismo_Tipo))]
        public async Task<IHttpActionResult> GetOrganismo_Tipo(decimal id)
        {
            try
            {

                Organismo_Tipo organismo_Tipo = await db.Organismo_Tipo.FindAsync(id);
                if (organismo_Tipo == null)
                {
                    return NotFound();
                }

                return Ok(organismo_Tipo);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // PUT: api/OrganismoTipo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganismo_Tipo(decimal id, Organismo_Tipo organismo_Tipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organismo_Tipo.IdOrganismoTipo)
            {
                return BadRequest();
            }

            db.Entry(organismo_Tipo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Organismo_TipoExists(id))
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

        // POST: api/OrganismoTipo
        [ResponseType(typeof(Organismo_Tipo))]
        public async Task<IHttpActionResult> PostOrganismo_Tipo(Organismo_Tipo organismo_Tipo)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Organismo_Tipo.Add(organismo_Tipo);
                await db.SaveChangesAsync();

                //return CreatedAtRoute("DefaultApi", new { id = organismo_Tipo.IdOrganismoTipo }, organismo_Tipo);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/OrganismoTipo/5
        [ResponseType(typeof(Organismo_Tipo))]
        public async Task<IHttpActionResult> DeleteOrganismo_Tipo(decimal id)
        {
            try
            {

                Organismo_Tipo organismo_Tipo = await db.Organismo_Tipo.FindAsync(id);
                if (organismo_Tipo == null)
                {
                    return NotFound();
                }

                db.Organismo_Tipo.Remove(organismo_Tipo);
                await db.SaveChangesAsync();

                return Ok(organismo_Tipo);

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

        private bool Organismo_TipoExists(decimal id)
        {
            return db.Organismo_Tipo.Count(e => e.IdOrganismoTipo == id) > 0;
        }
    }
}