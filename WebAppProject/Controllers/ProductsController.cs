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
    public class ProductsController : Controller
    {
        private WebAppProjectContext db = new WebAppProjectContext();


        #region Products For Customrs


        [AllowAnonymous]
        public ActionResult Index ()
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

            var allProducts = db.AllProducts.Include(p => p.Category);
            return View(allProducts.ToList());
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.AllProducts.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }


        [AllowAnonymous]
        public ActionResult ForMale()
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
            var male = db.AllProducts.Where(a=>a.Category.CategoryName.Contains("مردانه"));
            if (male.Count() > 0)
            {
                return View(male);
            }
            ViewBag.error = "محصولی در این دسته بندی وجود ندارد";
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForFemale()
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

            var female = db.AllProducts.Where(a => a.Category.CategoryName.Contains("زنانه"));
            if (female.Count() > 0)
            {
                return View(female);
            }
            ViewBag.error = "محصولی در این دسته بندی وجود ندارد";
            return View();
        }

        [AllowAnonymous]
        public ActionResult TopSell()
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

            #region FindTop8
            var TopRow = (from x in db.AllOrderDetails
                          group x by new { x.ProductId } into val
                          select new
                          {
                              val.Key.ProductId,
                              QuantitySum = val.Sum(s => s.Quantity)
                          }).OrderByDescending(i => i.QuantitySum).Take(20).ToList();

            List<ProductViewModel> db_ProductForTopList = new List<ProductViewModel>();
            foreach (var item in TopRow)
            {
                var findProdact = db.AllProducts.FirstOrDefault(a => a.ProductsId == item.ProductId);
                ProductViewModel viewModel = new ProductViewModel();

                viewModel.ProductsId = findProdact.ProductsId;
                viewModel.Image = findProdact.Image;
                viewModel.UnitPrice = findProdact.UnitPrice;
                viewModel.ProductName = findProdact.ProductName;
                viewModel.Quantity = item.QuantitySum;
                db_ProductForTopList.Add(viewModel);
            }
            #endregion

            //top sell
            return View(db_ProductForTopList.ToList());
        }
        #endregion

        [Authorize(Roles = "Emploee")]
        public ActionResult ProductDetails(int? id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.AllProducts.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        // GET: Products
        [Authorize(Roles = "Emploee")]
        public ActionResult ProductList()
        {
            var allProducts = db.AllProducts.Include(p => p.Category);
            var product = db.AllProducts.FirstOrDefault(a => a.ProductsId > 0);

            var search = db.AllOrderDetails.Where(a => a.ProductId == product.ProductsId);
            ViewBag.Quantity = search.Sum(a => a.Quantity);
            return View(allProducts.ToList());
        }

       

        // GET: Products/Create
        [Authorize(Roles = "Emploee")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.AllCategory, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Emploee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductsId,CategoryId,ProductName,UnitsInStock,UnitPrice,Image")] Products products, HttpPostedFileBase file , string describ)
        {
            products.Description = describ;
            products.Image = new byte[file.ContentLength];
            file.InputStream.Read(products.Image, 0, file.ContentLength);
            if (file != null)
            {
                
                db.AllProducts.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.AllCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Emploee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.AllProducts.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.AllCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Emploee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductsId,CategoryId,ProductName,UnitsInStock,UnitPrice,Image")] Products products, HttpPostedFileBase file,string describ)
        {
            products.Image = new byte[file.ContentLength];
            file.InputStream.Read(products.Image, 0, file.ContentLength);
            products.Description = describ;
            if (products.Description !=null && file !=null)
            {
                
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.AllCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Emploee")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.AllProducts.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.AllProducts.Find(id);
            db.AllProducts.Remove(products);
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
