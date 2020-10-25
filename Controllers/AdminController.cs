using InternetoweBazyDanych.Database;
using InternetoweBazyDanych.Models;
using InternetoweBazyDanych.Models.Book;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace InternetoweBazyDanych.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        BibliotekaEntities db = new BibliotekaEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllBooks(string search)
        {
            //return View(db.Ksiazki.ToList().OrderBy(ksiazka => ksiazka.idKsiazki).Where(a => a.czyAktywna == true));
            //toLower zmienia wielość liter na małe
            return View(db.Ksiazki.Where(ksiazka => ksiazka.czyAktywna == true && ksiazka.tytul.ToLower().Contains(search.ToLower()) || (search == "" || search == null)).ToList().OrderBy(ksiazka => ksiazka.idKsiazki));
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            var model = new Models.Book.AddAuthor();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAuthor(AddAuthor addAuthor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (db)
                    {

                        var row = new Autorzy
                        {
                            imie = addAuthor.imie,
                            nazwisko = addAuthor.nazwisko,
                            dataUrodzenia = addAuthor.dataUrodzenia,
                        };

                        db.Autorzy.Add(row);
                        db.SaveChanges();
                        ViewBag.Message = "Autor dodany";
                    }
                }
                catch (Exception)
                { }
            }
            return View(addAuthor);
        }

        public void FillAuthorsGenresLists()
        {
            var authorList = db.Autorzy.ToList().Select(autor => new { idAutora = autor.idAutora, tekst = autor.imie + " " + autor.nazwisko });
            ViewBag.AuthorList = new SelectList(authorList, "idAutora", "tekst");

            var genreList = db.Gatunki.ToList();
            ViewBag.GenreList = new SelectList(genreList, "idGatunku", "gatunek");
        }


        [HttpGet]
        public ActionResult CreateBook()
        {
            var model = new Models.Book.AddBook();

            FillAuthorsGenresLists();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBook(AddBook addBook)
        {
            FillAuthorsGenresLists();

            if (ModelState.IsValid)
            {
                try
                {
                    using (db)
                    {
                        var row = new Ksiazki
                        {
                            tytul = addBook.tytul,
                            opis = addBook.opis,
                            iloscStron = addBook.iloscStron,
                            dataWydania = addBook.dataWydania,
                            ilosc = addBook.ilosc,
                            idGatunku = addBook.idAutora,
                            idAutora = addBook.idAutora,
                            czyAktywna = true
                        };

                        db.Ksiazki.Add(row);
                        db.SaveChanges();
                        ViewBag.Message = "Książka dodana";
                    }
                }
                catch (Exception)
                { }
            }
            return View(addBook);
        }

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            var model = new DetailsBookViewModel();
            model = Services.BookMethods.GetBook(id);
            FillAuthorsGenresLists();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBook(int id, DetailsBookViewModel model)
        {
            FillAuthorsGenresLists();
            try
            {
                using (db)
                {
                    var row = db.Ksiazki.FirstOrDefault(k => k.idKsiazki == id);

                    if (row != null)
                    {
                        row.idKsiazki = id;
                        row.tytul = model.tytul;
                        row.opis = model.opis;
                        row.iloscStron = model.iloscStron;
                        row.dataWydania = model.dataWydania;
                        row.ilosc = model.ilosc;
                        row.idGatunku = model.idGatunku;
                        row.idAutora = model.idAutora;

                        db.SaveChanges();
                        ViewBag.Message = "Zapisano.";
                    }
                }

            }
            catch (Exception ex)
            { }
            return View(model);
        }


        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            var model = new DetailsBookViewModel();
            model = Services.BookMethods.GetBook(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteBook(int id, AddBook model)
        {
            try
            {
                using (db)
                {
                    var row = db.Ksiazki.FirstOrDefault(k => k.idKsiazki == id);

                    row.czyAktywna = false;

                    db.Ksiazki.AddOrUpdate(row);
                    db.SaveChanges();
                    return RedirectToAction("AllBooks", "Admin");
                }
            }
            catch (Exception ex)
            { }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            var model = new Models.RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    registerViewModel.hash = new Services.Authentication().GenerateHash(registerViewModel.haslo);
                    Uzytkownicy uzytkownik = new Uzytkownicy();
                    if (Services.CheckAccounts.checkLogin(registerViewModel.login))
                    {
                        ViewBag.Error = "Taki login już istineje";
                    }
                    else if (Services.CheckAccounts.checkEmail(registerViewModel.email))
                    {
                        ViewBag.Error = "Taki e-mail już istnieje.";
                    }
                    else if (registerViewModel.hash != null)
                    {
                        uzytkownik.login = registerViewModel.login;
                        uzytkownik.haslo = registerViewModel.hash;
                        uzytkownik.imie = registerViewModel.imie;
                        uzytkownik.nazwisko = registerViewModel.nazwisko;
                        uzytkownik.email = registerViewModel.email;
                        uzytkownik.adres = registerViewModel.adres;
                        uzytkownik.miasto = registerViewModel.miasto;
                        uzytkownik.kodPocztowy = registerViewModel.kodPocztowy;
                        uzytkownik.idRoli = 1;
                        uzytkownik.czyAktywny = true;
                        db.Uzytkownicy.Add(uzytkownik);
                        db.SaveChanges();
                        return RedirectToAction("AllUsers", "Admin");
                    }
                    else
                    {
                        ViewBag.Error = "Wystąpił błąd";
                    }

                }
                catch (Exception)
                { }

            }
            return View(registerViewModel);
        }

        public static RegisterViewModel GetUser(int id)
        {
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var user = db.Uzytkownicy.FirstOrDefault(f => f.idUzytkownika == id);

                    if (user != null)
                    {
                        return new RegisterViewModel
                        {
                            login = user.login,
                            imie = user.imie,
                            nazwisko = user.nazwisko,
                            email = user.email,
                            adres = user.adres,
                            miasto = user.miasto,
                            kodPocztowy = user.kodPocztowy,
                            rola = user.Role.rola
                        };
                    }

                }
            }
            catch (Exception ex)
            { }

            return null;

        }


        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var model = new RegisterViewModel();
            model = GetUser(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(int id, RegisterViewModel model)
        {
            try
            {
                using (db)
                {
                    model.hash = new Services.Authentication().GenerateHash(model.haslo);
                    var row = db.Uzytkownicy.FirstOrDefault(f => f.idUzytkownika == id);

                    if (row != null)
                    {
                        row.idUzytkownika = id;
                        row.login = model.login;
                        row.haslo = model.hash;
                        row.imie = model.imie;
                        row.nazwisko = model.nazwisko;
                        row.email = model.email;
                        row.adres = model.adres;
                        row.miasto = model.miasto;
                        row.kodPocztowy = model.kodPocztowy;
                        row.idRoli = 1;
                        row.czyAktywny = true;

                        db.SaveChanges();
                        ViewBag.Message = "Zapisano.";
                    }
                }

            }
            catch (Exception ex)
            { }
            return View(model);
        }


        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            var model = new RegisterViewModel();
            model = GetUser(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, RegisterViewModel model)
        {
            try
            {
                using (db)
                {
                    var row = db.Uzytkownicy.FirstOrDefault(f => f.idUzytkownika == id);

                    row.czyAktywny = false;

                    db.Uzytkownicy.AddOrUpdate(row);
                    db.SaveChanges();
                    return RedirectToAction("AllUsers", "Admin");
                }
            }
            catch (Exception ex)
            { }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddGenre()
        {
            var model = new Models.Book.GenresViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddGenre(GenresViewModel genresViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (db)
                    {
                        var row = new Gatunki
                        {
                            gatunek = genresViewModel.gatunek,
                        };

                        db.Gatunki.Add(row);
                        db.SaveChanges();
                        ViewBag.Message = "Zapisano.";
                    }
                }
                catch (Exception)
                { }
            }
            return View(genresViewModel);
        }

        public ActionResult AllUsers()
        {
            return View(db.Uzytkownicy.ToList().OrderBy(user => user.idUzytkownika).Where(rola => rola.idRoli == 1 && rola.czyAktywny == true));
        }

        [HttpGet]
        public ActionResult UsersRentalBooks()
        {
            var model = Services.BookMethods.GetUsersRentalList();
            return View(model);
        }
    }
}