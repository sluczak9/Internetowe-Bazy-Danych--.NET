using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models
{
    public class LoginViewModel
    {
        [Key]
        public int idUzytkownika { get; set; }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Wpisz login")]
        public string login { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Hasło musi mieć od 6 do maksymalnie 30 znaków")]
        [Required(ErrorMessage = "Wpisz hasło")]
        public string haslo { get; set; }
        [Display(Name = "Zapamiętaj mnie")]
        public bool remember { get; set; }

        public string hash { get; set; }
    }
}