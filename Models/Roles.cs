using InternetoweBazyDanych.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetoweBazyDanych.Models
{
    public class Roles
    {
        public static string GetRole(int id)
        {
            try
            {
                using (BibliotekaEntities db = new BibliotekaEntities())
                {
                    var role = db.Role.FirstOrDefault(x => x.idRoli == id);

                    if (role != null)
                    {
                        return role.rola;
                    }

                }
            }
            catch (Exception ex)
            { }
            return null;
        }
    }
}