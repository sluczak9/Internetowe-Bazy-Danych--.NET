using InternetoweBazyDanych.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Services
{
    public class BookMethods
    {

        public static Models.Book.DetailsBookViewModel GetBook(int id)
        {
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var book = db.Ksiazki.FirstOrDefault(k => k.idKsiazki == id);

                    if (book != null)
                    {
                        return new Models.Book.DetailsBookViewModel
                        {
                            tytul = book.tytul,
                            opis = book.opis,
                            iloscStron = book.iloscStron,
                            dataWydania = book.dataWydania, 
                            ilosc = book.ilosc,
                            gatunek = book.Gatunki.gatunek,
                            autor = book.Autorzy.imie + " " + book.Autorzy.nazwisko
                        };
                    }

                }
            }
            catch (Exception ex)
            { }

            return null;

        }

        public static Models.Book.Rental GetBookDetails(int id)
        {
            List<Models.Book.Rental> list = new List<Models.Book.Rental>();
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    //var rental = db.Wypozyczenia.ToList().Where(x => x.idWypozyczenia == id);
                    var rental = db.Wypozyczenia.FirstOrDefault(k => k.idWypozyczenia == id);

                    return new Models.Book.Rental
                        {
                            idWypozyczenia = id,
                            tytul = rental.Ksiazki.tytul,
                            opis = rental.Ksiazki.opis,
                            autor = rental.Ksiazki.Autorzy.imie + " " + rental.Ksiazki.Autorzy.nazwisko,
                            gatunek = rental.Ksiazki.Gatunki.gatunek,
                            terminWypozyczenia = rental.terminWypozyczenia,
                            terminZwrotu = rental.terminZwrotu,
                            status = rental.Statusy_Wypozyczen.status
                        }; 

                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public static List<Models.Book.Rental> GetRentalList()
        {
            List<Models.Book.Rental> list = new List<Models.Book.Rental>();
            int userId = Services.UserMethods.GetUserID(System.Web.HttpContext.Current.User.Identity.Name);

            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var rental = db.Wypozyczenia.ToList().Where(x => x.idUzytkownika == userId && x.idStatusu == 1);


                    foreach (var x in rental)
                    {
                        list.Add(
                        new Models.Book.Rental
                        {
                            idWypozyczenia = x.idWypozyczenia,
                            tytul = x.Ksiazki.tytul,
                            autor = x.Ksiazki.Autorzy.imie + " " + x.Ksiazki.Autorzy.nazwisko,
                            terminWypozyczenia = x.terminWypozyczenia,
                            terminZwrotu = x.terminZwrotu,
                            status = x.Statusy_Wypozyczen.status
                        });
                    }
                }
            }
            catch (Exception ex)
            { }

            return list;
        }

        public static List<Models.Book.Rental> GetRentalHistoryList()
        {
            List<Models.Book.Rental> list = new List<Models.Book.Rental>();
            int userId = Services.UserMethods.GetUserID(System.Web.HttpContext.Current.User.Identity.Name);

            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var rental = db.Wypozyczenia.ToList().Where(x => x.idUzytkownika == userId && x.idStatusu == 2);


                    foreach (var x in rental)
                    {
                        list.Add(
                        new Models.Book.Rental
                        {
                            idWypozyczenia = x.idWypozyczenia,
                            tytul = x.Ksiazki.tytul,
                            autor = x.Ksiazki.Autorzy.imie + " " + x.Ksiazki.Autorzy.nazwisko,
                            terminWypozyczenia = x.terminWypozyczenia,
                            terminZwrotu = x.terminZwrotu,
                            status = x.Statusy_Wypozyczen.status
                        });
                    }
                }
            }
            catch (Exception ex)
            { }

            return list;
        }


        public static List<Models.Book.Rental> GetUsersRentalList()
        {
            List<Models.Book.Rental> list = new List<Models.Book.Rental>();
           

            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    //var rental = db.Wypozyczenia.ToList().Where(x => x.idUzytkownika == userId);
                    var rental = db.Wypozyczenia.ToList();


                    foreach (var x in rental)
                    {
                        list.Add(
                        new Models.Book.Rental
                        {
                            login = x.Uzytkownicy.login,
                            idWypozyczenia = x.idWypozyczenia,
                            tytul = x.Ksiazki.tytul,
                            autor = x.Ksiazki.Autorzy.imie + " " + x.Ksiazki.Autorzy.nazwisko,
                            terminWypozyczenia = x.terminWypozyczenia,
                            terminZwrotu = x.terminZwrotu,
                            status = x.Statusy_Wypozyczen.status
                        });
                    }
                }
            }
            catch (Exception ex)
            { }

            return list;
        }

        /*
        public static bool checkBookAmount(int amount) 
        {
            using (BibliotekaEntities db = new BibliotekaEntities())
            {
                var Verify = db.Ksiazki.FirstOrDefault(user => user.ilosc == amount);
                if (Verify.ilosc < amount )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        } 
        */
    }
}
