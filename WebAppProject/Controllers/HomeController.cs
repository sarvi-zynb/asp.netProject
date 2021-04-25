using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        WebAppProjectContext db;
        public HomeController()
        {
            db = new WebAppProjectContext();
        }
        public ActionResult Index()
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

            var procudtList = db.AllProducts.ToList();
            var row = (from x in db.AllOrderDetails
                       group x by new { x.ProductId } into val
                       select new
                       {
                           val.Key.ProductId,
                           QuantitySum = val.Sum(s => s.Quantity)
                       }).OrderByDescending(i => i.QuantitySum).ToList();

            #region FindTop8
            var TopRow = (from x in db.AllOrderDetails
                    group x by new { x.ProductId } into val
                    select new
                    {
                        val.Key.ProductId,
                        QuantitySum = val.Sum(s => s.Quantity)
                    }).OrderByDescending(i => i.QuantitySum).Take(8).ToList();

            List<ProductViewModel> db_ProductForTopList = new List<ProductViewModel>();
            foreach (var item in TopRow)
            {
                var findProdact = db.AllProducts.FirstOrDefault(a=>a.ProductsId == item.ProductId);
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
            ViewBag.topSell = db_ProductForTopList.ToList();
            //4 newest product
            ViewBag.newest = db.AllProducts.OrderByDescending(a=>a.ProductsId).Take(4).ToList();
            return View(db.AllProducts.ToList());
        }
        [HttpGet]
        public ActionResult Search(string q)
        {
          var search =  db.AllProducts.Where(a => a.ProductName.Contains(q));
            if (search.Count() > 0)
            {
                return View(search);
            }
            ViewBag.error = "محصول مورد نظر یافت نشد";
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }


        public ActionResult FAQ()
        {
            return View();
        }

    }
}