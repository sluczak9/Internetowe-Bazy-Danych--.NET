using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models.Book
{
    public class AddAuthor
    {
        public int idAutora { get; set; }

        [Required(ErrorMessage = "Wpisz Imię")]
        [Display(Name = "Imię")]
        public string imie { get; set; }

        [Required(ErrorMessage = "Wpisz Nazwisko")]
        [Display(Name = "Nazwisko")]
        public string nazwisko { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Wpisz datę urodzenia")]
        [Display(Name = "Data urodzenia")]
        public System.DateTime dataUrodzenia { get; set; }
    }
}