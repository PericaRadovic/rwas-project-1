using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParketarskaRadnja.Models
{
    public class DetaljiNarudzbine
    {
        [Key]
        public int DetaljNarudzbineID { get; set; }
        public int NarudzbinaID { get; set; }
        public int ProizvodID { get; set; }
        public int Kolicina { get; set; }
        public decimal CenaPoJedinici { get; set; }

        public Narudzbine Narudzbine { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}