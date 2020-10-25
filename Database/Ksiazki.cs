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
    
    public partial class Ksiazki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ksiazki()
        {
            this.Wypozyczenia = new HashSet<Wypozyczenia>();
        }
    
        public int idKsiazki { get; set; }
        public string tytul { get; set; }
        public string opis { get; set; }
        public int iloscStron { get; set; }
        public System.DateTime dataWydania { get; set; }
        public int ilosc { get; set; }
        public int idGatunku { get; set; }
        public int idAutora { get; set; }
        public Nullable<bool> czyAktywna { get; set; }
    
        public virtual Autorzy Autorzy { get; set; }
        public virtual Gatunki Gatunki { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}
