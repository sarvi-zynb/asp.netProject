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
    
    public class CustomersController : Controller
    {
        private WebAppProjectContext db = new WebAppProjectContext();

        // GET: Customers

        [Authorize(Roles = "Emploee")]
        public ActionResult Index()
        {
           
            return View(db.AllCustomers.ToList());
        }

        [Authorize(Roles = "Customers")]
        public ActionResult EditProfile(int id)
        {


            #region FindRole
            //Find User Role from Cockie
            var UserOREmploeeIsAlive = User.Identity.Name;
            var searchUser = db.AllCustomers.FirstOrDefault(a => a.Email == UserOREmploeeIsAlive);
            var searchEmploee = db.AllEmploee.FirstOrDefault(a => a.Email == UserOREmploeeIsAlive);
            if (searchUser != null)
            {
                ViewBag.UserIsAlive = searchUser.FullName;
            }
            if (searchEmploee != null)
            {
                ViewBag.EmploeeIsAlive = searchEmploee.FullName;
            }
            #endregion
            if (searchUser != null)
            {
                return View(searchUser);
            }
            return View();
        }

        [Authorize(Roles = "Customers")]
        public ActionResult CustomerDetail()
        {
            #region FindRole
            //Find User Role from Cockie
            var UserOREmploeeIsAlive = User.Identity.Name;
            var searchUser = db.AllCustomers.FirstOrDefault(a => a.Email == UserOREmploeeIsAlive);
            var searchEmploee = db.AllEmploee.FirstOrDefault(a => a.Email == UserOREmploeeIsAlive);
            if (searchUser != null)
            {
                ViewBag.UserIsAlive = searchUser.FullName;
            }
            if (searchEmploee != null)
            {
                ViewBag.EmploeeIsAlive = searchEmploee.FullName;
            }
            #endregion
            if (searchUser != null)
            {
                var Takes = db.AllOrders.Where(a => a.CustomersId == searchUser.CustomersId);
                ViewBag.booksTake = Takes.OrderByDescending(a => a.OrdersId).ToList();
                #region FindLastshop
                var query = (from od in db.AllOrderDetails
                             join oo in db.AllOrders on od.OrdersId equals oo.OrdersId

                             where oo.Customers.Email == searchUser.Email

                             select new  // Projection to Anonymous type
                             {
                                 CustomerFullName = oo.Customers.FullName,
                                 CustomerOrderDate = oo.OrderDate,
                                 CustomerReciveDate = oo.ReciveDate,
                                 ProductName = od.Products.ProductName,
                                 Image = od.Products.Image,
                                 ProductQuantity = od.Quantity,
                                 ProductPrice = od.UnitPrice, // seven value
                             }).ToList();

                List<CustomersViewModel> db_LastCustomerShopList = new List<CustomersViewModel>();
                foreach (var item in query)
                {
                    CustomersViewModel viewModel = new CustomersViewModel();

                    viewModel.CustomerFullName = item.CustomerFullName;
                    viewModel.OrderDate = item.CustomerOrderDate;
                    viewModel.ProductName = item.ProductName;
                    viewModel.Image = item.Image;
                    viewModel.ReciveDate = item.CustomerReciveDate;
                    viewModel.Quantity = item.ProductQuantity;
                    viewModel.UnitPrice = item.ProductPrice;
                    db_LastCustomerShopList.Add(viewModel);
                }

                ViewBag.lastOrders = db_LastCustomerShopList.ToList();
                var allCustomer = db.AllCustomers.Include(c => c.Role);
                return View(searchUser);
            }
            #endregion
            ViewBag.Error = "User Not Found";
            var allCustomers = db.AllCustomers.Include(c => c.Role);
            return View();
        }



        // GET: Customers/Details/5
        [Authorize(Roles = "Emploee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.AllCustomers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Emploee")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Emploee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomersId,RoleId,FullName,Gender,Address,PhoneNumber,Email,Password")] Customers customers)
        {
            customers.RoleId = 2;
            if (ModelState.IsValid)
            {
                db.AllCustomers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", customers.RoleId);
            return View(customers);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Emploee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.AllCustomers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", customers.RoleId);
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Emploee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomersId,RoleId,FullName,Gender,Address,PhoneNumber,Email,Password")] Customers customers)
        {
            customers.RoleId = 2;
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.AllRole, "RoleId", "RoleName", customers.RoleId);
            return View(customers);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Emploee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.AllCustomers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.AllCustomers.Find(id);
            db.AllCustomers.Remove(customers);
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
