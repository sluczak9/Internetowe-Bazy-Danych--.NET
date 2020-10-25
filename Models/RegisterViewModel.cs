using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models
{
    public class RegisterViewModel
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

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Wpisz imię")]
        public string imie { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Wpisz nazwisko")]
        public string nazwisko { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres E-mail")]
        public string email { get; set; }
        [Required(ErrorMessage = "Wpisz adres")]

        [Display(Name = "Adres")]
        public string adres { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Wpisz miasto")]
        public string miasto { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Wpisz kod pocztowy")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Kod pocztowy musi mieć 6 znaków")]
        public string kodPocztowy { get; set; }

        public string rola { get; set; }

        public bool czyAktywny { get; set; }

        public string hash { get; set; }


    }
}