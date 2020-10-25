﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models.Book
{
    public class Rental
    {
        [Key]
        public int idWypozyczenia { get; set; }

        public int idKsiazki { get; set; }
        [Display(Name = "Tytuł")]
        public string tytul { get; set; }
        [Display(Name = "Opis")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Opis musi mieć od dwóch do 255 znaków")]
        public string opis { get; set; }
        [Display(Name = "Ilość stron")]
        [Range(1, int.MaxValue, ErrorMessage = "Liczba stron nie może wynosić 0")]
        public int iloscStron { get; set; }

        [Display(Name = "Gatunek")]
        public string gatunek { get; set; }

        [Display(Name = "Autor")]
        public string autor { get; set; }

        [Display(Name = "Data wypożyczenia")]
        [DataType(DataType.Date)]
        public DateTime terminWypozyczenia { get; set; }

        [Display(Name = "Termin zwrotu")]
        [DataType(DataType.Date)]
        public DateTime terminZwrotu { get; set; }

        [Display(Name = "Status wypożyczenia")]
        public string status { get; set; }

        [Display(Name = "Użytkownik")]
        public string login { get; set; }
    }
}