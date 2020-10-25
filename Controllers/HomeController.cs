using InternetoweBazyDanych.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetoweBazyDanych.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BibliotekaEntities db = new BibliotekaEntities();
            return View(db.Ksiazki.ToList().OrderByDescending(ksiazka => ksiazka.idKsiazki).Where(a => a.czyAktywna == true).Take(5));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Dane studenta";

            return View();
        }
    }
}