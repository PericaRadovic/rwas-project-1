using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParketarskaRadnja.Models
{
    public class Klijent
    {
        [Key]
        public int KlijentID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }

        public ICollection<Narudzbine> Narudzbine { get; set; }
    }
}