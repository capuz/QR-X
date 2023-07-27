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
    public class MenuController : ApiController
    {
        private domEntities db = new domEntities();
        private clsAuditoria Log = new clsAuditoria();

        // GET: api/Menu
        public IQueryable<Menu> GetMenu()
        {
            try
            {

                return db.Menu;

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        // GET: api/Menu/5
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> GetMenu(decimal id)
        {
            try
            {

                Menu menu = await db.Menu.FindAsync(id);
                if (menu == null)
                {
                    return NotFound();
                }

                return Ok(menu);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        public IEnumerable<USP_MENU_Select_HomePage_Result> GetHomePage()
        {
            try
            {

                return db.USP_MENU_Select_HomePage().AsEnumerable();

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        public IEnumerable<USP_MENU_Select_Mantenedor_Result> GetMantenedor()
        {
            try
            {

                return db.USP_MENU_Select_Mantenedor().AsEnumerable();

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }

        public IEnumerable<USP_MENU_Select_Parent_Result> GetParent()
        {
            try
            {

                return db.USP_MENU_Select_Parent().AsEnumerable();

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return null;
            }
        }


        // PUT: api/Menu/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMenu(decimal id, string usuario, Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.IdMenu)
            {
                return BadRequest();
            }

            db.Entry(menu).State = EntityState.Modified;

            try
            {

                //Auditoria
                Menu obj = db.Menu.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Menu", 2);
                //Auditoria

                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menu
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> PostMenu(string usuario, Menu menu)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Menu.Add(menu);
                await db.SaveChangesAsync();

                //Auditoria
                Menu obj = db.Menu.Find(menu.IdMenu);
                Log.Auditoria(obj, int.Parse(menu.IdMenu.ToString()), usuario, "Menu", 1);
                //Auditoria

                //return CreatedAtRoute("DefaultApi", new { id = menu.IdMenu }, menu);
                return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                Log.Log(3, 5, Log.GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // DELETE: api/Menu/5
        [ResponseType(typeof(Menu))]
        public async Task<IHttpActionResult> DeleteMenu(decimal id, string usuario)
        {
            try
            {

                Menu menu = await db.Menu.FindAsync(id);
                if (menu == null)
                {
                    return NotFound();
                }

                //Auditoria
                Menu obj = db.Menu.Find(id);
                Log.Auditoria(obj, int.Parse(id.ToString()), usuario, "Menu", 3);
                //Auditoria

                db.Menu.Remove(menu);
                await db.SaveChangesAsync();

                return Ok(menu);

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

        private bool MenuExists(decimal id)
        {
            return db.Menu.Count(e => e.IdMenu == id) > 0;
        }
    }
}