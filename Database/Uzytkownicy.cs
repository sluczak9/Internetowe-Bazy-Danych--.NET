//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternetoweBazyDanych.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uzytkownicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uzytkownicy()
        {
            this.Wypozyczenia = new HashSet<Wypozyczenia>();
        }
    
        public int idUzytkownika { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public string email { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string adres { get; set; }
        public string miasto { get; set; }
        public string kodPocztowy { get; set; }
        public int idRoli { get; set; }
        public bool czyAktywny { get; set; }
    
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}
