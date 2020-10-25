using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models.Book
{
    public class AuthorsViewModel
    {
        public int idAutora { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }

       // public string pelneImie { get { return this.imie + " " + this.nazwisko; } }
    }
}