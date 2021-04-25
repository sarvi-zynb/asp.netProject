using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Emploee")]
    public class OrdersController : Controller
    {
        private WebAppProjectContext db = new WebAppProjectContext();

        // GET: Orders
        public ActionResult Index()
        {
            var allOrders = db.AllOrders.Include(o => o.Customers);
            return View(allOrders.ToList());
        }

        
        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.AllOrders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.AllOrders.Find(id);
            db.AllOrders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
