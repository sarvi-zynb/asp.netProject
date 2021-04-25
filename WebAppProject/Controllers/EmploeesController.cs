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
    public class EmploeesController : Controller
    {
        private WebAppProjectContext db = new WebAppProjectContext();

        // GET: Emploees
        public ActionResult Index()
        {
            var allEmploee = db.AllEmploee.Include(e => e.Role);
            return View(allEmploee.ToList());
        }

        // GET: Emploees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = db.AllEmploee.Find(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }

        // GET: Emploees/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName");
            return View();
        }

        // POST: Emploees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploeeId,RoleId,FullName,PhoneNumber,Email,Password")] Emploee emploee)
        {
            if (ModelState.IsValid)
            {
                db.AllEmploee.Add(emploee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", emploee.RoleId);
            return View(emploee);
        }

        // GET: Emploees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = db.AllEmploee.Find(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", emploee.RoleId);
            return View(emploee);
        }

        // POST: Emploees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploeeId,RoleId,FullName,PhoneNumber,Email,Password")] Emploee emploee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", emploee.RoleId);
            return View(emploee);
        }

        // GET: Emploees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = db.AllEmploee.Find(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }

        // POST: Emploees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emploee emploee = db.AllEmploee.Find(id);
            db.AllEmploee.Remove(emploee);
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
