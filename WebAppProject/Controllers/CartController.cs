using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    
    public class CartController : Controller
    {
      
        private WebAppProjectContext db = new WebAppProjectContext();

        // GET: Cart


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

            List<ShopCartViewModel> model = new List<ShopCartViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                foreach (var item in cart)
                {
                    var product = db.AllProducts.Find(item.ProductId);
                    model.Add(new ShopCartViewModel()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        Title = product.ProductName,
                        Price = product.UnitPrice,
                        Sum = item.Count * product.UnitPrice,
                        Image = product.Image

                    });
                }
            }
            if (Session["ShopCart"] == null)
            {
                ViewBag.error = "سبد خرید شما خالی است";
                return View();
            }
            return View(model);
        }


        [Authorize(Roles = "Customers")]
        public ActionResult Pay(Orders orders, OrderDetails orderDetails)
        {
            if (Session["ShopCart"] != null)
            {
               var CustomerIsAlive = User.Identity.Name;
               var searchUser = db.AllCustomers.FirstOrDefault(a => a.Email == CustomerIsAlive);
                if (searchUser != null)
                {
                    orders.CustomersId = searchUser.CustomersId;
                    orders.OrderDate = DateTime.Now;
                    orders.ReciveDate = DateTime.Now.AddDays(10);
                    db.AllOrders.Add(orders);
                    db.SaveChanges();

                    //save to orderDetails
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var item in cart)
                    {
                        var product = db.AllProducts.Find(item.ProductId);

                        orderDetails.OrdersId = orders.OrdersId;
                        orderDetails.ProductId = item.ProductId;
                        orderDetails.Quantity = item.Count;
                        orderDetails.UnitPrice = product.UnitPrice;
                        db.AllOrderDetails.Add(orderDetails);
                        db.SaveChanges();
                    }
                    
                }

            }
            Session["ShopCart"] = null;
            return View();
        }


        [Authorize(Roles = "Customers")]
        public ActionResult DeleteCartItem(int id)
        {
            List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
            int index = cart.FindIndex(a=>a.ProductId == id);
            cart.RemoveAt(index);
            Session["ShopCart"] = cart;
            return RedirectToAction("Index");
        }

        



        public int AddToCart(int id)
        {
            List<ShopCartItem> cart = new List<ShopCartItem>();

            if (Session["ShopCart"] != null)
            {
                cart = Session["ShopCart"] as List<ShopCartItem>;
            }
            if (cart.Any(a=>a.ProductId == id))
            {
                int index = cart.FindIndex(a => a.ProductId == id);
                cart[index].Count++;
            }
            else
            {
                cart.Add(new ShopCartItem()
                {
                    ProductId = id,
                    Count = 1
                });
            }

            Session["ShopCart"] = cart;

            return cart.Sum(a=>a.Count);
        }

        public int ShopCartCount()
        {
            int count = 0;

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                count = cart.Sum(p => p.Count);
            }

            return count;
        }
    }
}