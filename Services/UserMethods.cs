using InternetoweBazyDanych.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Services
{
    public class UserMethods
    {
        public static Models.RegisterViewModel GetUser(int ID)
        {
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var user = db.Uzytkownicy.FirstOrDefault(f => f.idUzytkownika == ID);

                    if (user != null)
                    {
                        return new Models.RegisterViewModel
                        {
                            idUzytkownika = user.idUzytkownika,
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

        public static int GetUserID(string login)
        {
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var user = db.Uzytkownicy.FirstOrDefault(f => f.login == login && (f.czyAktywny == true));

                    if (user != null)
                    {
                        return user.idUzytkownika;
                    }
                }
            }
            catch (Exception ex)
            { }
            return 0;
        }
    }
}