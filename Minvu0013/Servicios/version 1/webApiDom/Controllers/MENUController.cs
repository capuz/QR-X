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
    public class MENUController : ApiController
    {
        private domEntities db = new domEntities();

        // GET: api/MENU/GetMenu
        public IQueryable<MENU> GetMenu()
        {
            return db.MENU;
        }

        // GET: api/MENU/GetParents
        public IEnumerable<object> GetParents()
        {
            const decimal PARENT_LVL1 = 1;
            var result = db.MENU
                .GroupJoin(
                      db.MENU,
                      child => child.IdMenuPadre,
                      parent => parent.IdMenu,
                      (x, y) => new { Child = x, Parent = y })
                .SelectMany(
                      x => x.Parent.DefaultIfEmpty(),
                      (x, y) => new {
                          IdMenuPadre = x.Child.IdMenu,
                          NombreMenuPadre = x.Child.Nombre
                      })
                .DistinctBy(x => x.NombreMenuPadre)
                .Where(x => x.IdMenuPadre == PARENT_LVL1);

            return result;
        }

        // GET: api/MENU/GetMENU/5
        [ResponseType(typeof(MENU))]
        public async Task<IHttpActionResult> GetMenu(decimal id)
        {
            MENU mENU = await db.MENU.FindAsync(id);
            if (mENU == null)
            {
                return NotFound();
            }

            return Ok(mENU);
        }

        public IEnumerable<USP_MENU_Select_HomePage_Result> GetHomePage()
        {
            return db.USP_MENU_Select_HomePage().AsEnumerable();
        }

        public IEnumerable<USP_MENU_Select_Mantenedor_Result> GetMantenedor()
        {
            return db.USP_MENU_Select_Mantenedor().AsEnumerable();
        }

        // PUT: api/MENU/PutMENU/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMENU(decimal id, MENU mENU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mENU.IdMenu)
            {
                return BadRequest();
            }

            db.Entry(mENU).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MENUExists(id))
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

        // POST: api/MENU/POSTMENU
        [ResponseType(typeof(MENU))]
        public async Task<IHttpActionResult> PostMENU(MENU mENU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MENU.Add(mENU);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mENU.IdMenu }, mENU);
        }

        // DELETE: api/MENU/DeleteMENU/5
        [ResponseType(typeof(MENU))]
        public async Task<IHttpActionResult> DeleteMENU(decimal id)
        {
            MENU mENU = await db.MENU.FindAsync(id);
            if (mENU == null)
            {
                return NotFound();
            }

            db.MENU.Remove(mENU);
            await db.SaveChangesAsync();

            return Ok(mENU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MENUExists(decimal id)
        {
            return db.MENU.Count(e => e.IdMenu == id) > 0;
        }

    }
}