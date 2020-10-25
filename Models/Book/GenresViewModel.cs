using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models.Book
{
    public class GenresViewModel
    {
        [Key]
        public int idGatunku { get; set; }

        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "Wpisz nazwę gatunkę")]
        public string gatunek { get; set; }
    }
}