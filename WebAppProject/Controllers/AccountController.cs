using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        WebAppProjectContext db;
        public AccountController()
        {
            db = new WebAppProjectContext();
        }
        // GET: Account
        
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //search for user by email
                var FindEmail = db.AllCustomers.FirstOrDefault(a => a.Email == model.Email);
                if (FindEmail != null)
                {
                    if (FindEmail.Email == model.Email && FindEmail.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return Redirect(FormsAuthentication.DefaultUrl);
                    }
                }

                //search for emploee by email
                var FindEmailEmploee = db.AllEmploee.FirstOrDefault(a => a.Email == model.Email);
                if (FindEmailEmploee != null)
                {
                    if (FindEmailEmploee.Email == model.Email && FindEmailEmploee.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return Redirect(FormsAuthentication.DefaultUrl);
                    }
                }
                else
                {
                    ViewBag.error = "ایمیل یا پسورد اشتباه است";
                    return View();
                }

            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customers customers)
        {
            customers.RoleId = 2;
            if (ModelState.IsValid)
            {
                db.AllCustomers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("index" ,"home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword(Customers customers , string email)
        {
            if (customers.Email != null)
            {
                //search the eamil in DB
                db.AllCustomers.Any(a => a.Email == customers.Email);
                ViewBag.Message = "یک ایمیل حاوی پسورد شما به آدرس ایمیل شما ارسال شد";
                return View();
            }
            ViewBag.MessageError = "ایمیل وارد شده اشتباه است";
            return View();
        }
    }
}