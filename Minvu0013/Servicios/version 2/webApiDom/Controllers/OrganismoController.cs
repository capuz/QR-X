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
    public class OrganismoController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/Organismo
        public IQueryable<Organismo> GetOrganismo()
        {
            try
            {

                return db.Organismo;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/Organismo/5
        [ResponseType(typeof(Organismo))]
        public async Task<IHttpActionResult> GetOrganismo(decimal id)
        {
            try
            {

                Organismo organismo = await db.Organismo.FindAsync(id);
                if (organismo == null)
                {
                    return NotFound();
                }

                return Ok(organismo);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        public IEnumerable<USP_ORGANISMO_Select_Filtro_Result> GetFiltro(string param1)
        {
            try
            {
                return db.USP_ORGANISMO_Select_Filtro(param1).AsEnumerable();
            }
            catch
            {
                return null;
            }
        }

        // PUT: api/Organismo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganismo(decimal id, string usuario, Organismo organismo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organismo.IdOrganismo)
            {
                return BadRequest();
            }

            db.Entry(organismo).State = EntityState.Modified;

            try
            {

                //Auditoria
                Organismo obj = db.Organismo.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Organismo", 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganismoExists(id))
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

        // POST: api/Organismo
        [ResponseType(typeof(Organismo))]
        public async Task<IHttpActionResult> PostOrganismo(string usuario, Organismo organismo)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Organismo.Add(organismo);
                await db.SaveChangesAsync();

                //Auditoria
                Organismo obj = db.Organismo.Find(organismo.IdOrganismo);
                Log.Auditoria(obj, int.Parse(organismo.IdOrganismo.ToString()), usuario, "Organismo", 1);
                //Auditoria

                //return CreatedAtRoute("DefaultApi", new { id = organismo.IdOrganismo }, organismo);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Organismo/5
        [ResponseType(typeof(Organismo))]
        public async Task<IHttpActionResult> DeleteOrganismo(decimal id, string usuario)
        {
            try
            {

                Organismo organismo = await db.Organismo.FindAsync(id);
                if (organismo == null)
                {
                    return NotFound();
                }

                //Auditoria
                Organismo obj = db.Organismo.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Organismo", 3);
                //Auditoria

                organismo.Activo = false;
                await db.SaveChangesAsync();

                return Ok(organismo);

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

        private bool OrganismoExists(decimal id)
        {
            return db.Organismo.Count(e => e.IdOrganismo == id) > 0;
        }
    }
}