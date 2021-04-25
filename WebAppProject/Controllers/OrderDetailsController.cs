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
    public class OrderDetailsController : Controller
    {
        private WebAppProjectContext db = new WebAppProjectContext();

        // GET: User
        public ActionResult Index()
        {
            var allOrderDetails = db.AllOrderDetails.Include(o => o.Products);
            return View(allOrderDetails.ToList());
        }

       
        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = db.AllOrderDetails.Find(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetails orderDetails = db.AllOrderDetails.Find(id);
            db.AllOrderDetails.Remove(orderDetails);
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
