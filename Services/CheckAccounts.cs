using InternetoweBazyDanych.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Services
{
    public class CheckAccounts
    {
        //BibliotekaEntities db = new BibliotekaEntities();
        public static bool checkLogin(string login) // true jak istanieje w bazie 
        {
            using (BibliotekaEntities db = new BibliotekaEntities())
            {
                var Verify = db.Uzytkownicy.FirstOrDefault(user => user.login == login);
                if (Verify == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool checkEmail(string email)
        {
            using (BibliotekaEntities db = new BibliotekaEntities())
            {
                var Verify = db.Uzytkownicy.FirstOrDefault(user => user.email == email);
                if (Verify == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}