using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParketarskaRadnja.Models
{
    public class Magacin
    {
        [Key]
        public int StanjeID { get; set; }
        public int ProizvodID { get; set; }
        public int KolicinaNaZalihi { get; set; }

        public Proizvod Proizvod { get; set; }
    }
}