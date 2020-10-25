using InternetoweBazyDanych.Database;
using InternetoweBazyDanych.Models.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetoweBazyDanych.Controllers
{
    public class UserController : Controller
    {
        BibliotekaEntities db = new BibliotekaEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllBooks(string search)
        {
            //toLower zmienia wielość liter na małe
            return View(db.Ksiazki.Where(ksiazka => ksiazka.czyAktywna == true && (ksiazka.tytul.ToLower().Contains(search.ToLower()) || (search == "" || search == null))).ToList().OrderBy(ksiazka => ksiazka.idKsiazki));
        }

        

        [HttpGet]
        public ActionResult BookDetails(int id)
        {
            var model = new DetailsBookViewModel();
            model = Services.BookMethods.GetBook(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult BookDetails(int id, Wypozyczenia wypozyczenia)
        {
            var model = Services.BookMethods.GetBook(id);
            DateTime today = DateTime.Now.Date;
            try
            {
                using (db)
                {
                    if (model.ilosc >= 1)
                    {
                        model.ilosc = model.ilosc - 1;
                        int userId = Services.UserMethods.GetUserID(System.Web.HttpContext.Current.User.Identity.Name);
                        var row = db.Ksiazki.FirstOrDefault(k => k.idKsiazki == id);

                        wypozyczenia.terminWypozyczenia = today;
                        wypozyczenia.terminZwrotu = today.AddDays(30);
                        wypozyczenia.idUzytkownika = userId;
                        wypozyczenia.idStatusu = 1;
                        wypozyczenia.idKsiazki = id;
                        row.ilosc = model.ilosc;
                        db.Wypozyczenia.Add(wypozyczenia);
                        db.Ksiazki.AddOrUpdate(row);
                        db.SaveChanges();
                        ViewBag.Message = "Wypożyczono";
                    }
                    else
                    {
                        ViewBag.Message = "Brak książek na stanie";
                    }
                    
                }
            }
            catch (Exception ex)
            { }
            return View(model);
        }

        [HttpGet]
        public ActionResult RentalBooks()
        {
            var model = Services.BookMethods.GetRentalList();
            return View(model);
        }

        [HttpGet]
        public ActionResult ReturnBook(int id)
        {
            var model = new Rental();
            model = Services.BookMethods.GetBookDetails(id);
            return View(model); 
        }

        [HttpPost]
         public ActionResult ReturnBook(int id, Rental model)
         {
            var book = new Ksiazki();
            try
             {
                using (db)
                 {
                     var row = db.Wypozyczenia.FirstOrDefault(f => f.idWypozyczenia == id);
                    var amount = db.Ksiazki.FirstOrDefault(f => f.idKsiazki == row.idKsiazki);

                    row.idStatusu = 2;
                    amount.ilosc += 1;

                    db.Wypozyczenia.AddOrUpdate(row);
                    db.Ksiazki.AddOrUpdate(amount);
                    db.SaveChanges();
                    return RedirectToAction("RentalBooksHistory", "User");
                }
             }
             catch (Exception ex)
             { }
             return View(model);
         }

        [HttpGet]
        public ActionResult RentalBooksHistory()
        {
            var model = Services.BookMethods.GetRentalHistoryList();
            return View(model);
        }

    }
}