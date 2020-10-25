using InternetoweBazyDanych.Database;
using InternetoweBazyDanych.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InternetoweBazyDanych.Controllers
{
    public class AccountController : Controller
    {
        BibliotekaEntities db = new BibliotekaEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details()
        {
            int id = Services.UserMethods.GetUserID(System.Web.HttpContext.Current.User.Identity.Name);
            if (Request.IsAuthenticated && id != 0)
            // Database.UsersEntity.GetUserID(System.Web.HttpContext.Current.User.Identity.Name)
            {
                var model = new Models.RegisterViewModel();
                model = Services.UserMethods.GetUser(Services.UserMethods.GetUserID(System.Web.HttpContext.Current.User.Identity.Name));

                return View(model);
            }
            else
            {
                RedirectToAction("Login", "Account");
            }
            return View();
        }




        [HttpGet]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                RedirectToAction("Index", "Home");
            }
            else
            {
                var model = new Models.LoginViewModel();
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BibliotekaEntities())
                {
                    var Verify = db.Uzytkownicy.FirstOrDefault(user => user.login == model.login);
                    if (Verify == null)
                    {
                        ViewBag.Error = "Niepoprawny login lub hasło.";
                    }
                    else if (Verify.czyAktywny == false)
                    {
                        ViewBag.Error = "Konto jest dezaktywowane.";
                    }
                    else
                    {
                        model.hash = new Services.Authentication().GenerateHash(model.haslo);
                        if (Verify.haslo == model.hash)
                        {
                            FormsAuthentication.SignOut();
                            Session.Abandon();

                            Session["imie"] = Verify.login;
                            Session["email"] = Verify.email;
                            Session["IdUzytkownika"] = Verify.idUzytkownika;

                            //FormsAuthentication.SetAuthCookie(Verify.u_login, model.remember);
                            // https://stackoverflow.com/questions/1385042/asp-net-mvc-forms-authentication-authorize-attribute-simple-roles link
                            var authTicket = new FormsAuthenticationTicket(
                                                                        1,                             // version 
                                                                        model.login,                      // user name
                                                                        DateTime.Now,                  // created
                                                                        DateTime.Now.AddMinutes(30),   // expires
                                                                        model.remember,                    // persistent?
                                                                        Models.Roles.GetRole(Verify.idRoli)  // can be used to store roles
                                                                        );

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                            //ViewBag.Error = "Zalogowano!";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "Niepoprawne logowanie";
                        }
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}