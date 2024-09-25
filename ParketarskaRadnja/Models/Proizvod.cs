using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParketarskaRadnja.Models
{
    public class Proizvod
    {
        [Key]
        public int ProizvodID { get; set; }
        public string NazivProizvoda { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }

        public ICollection<DetaljiNarudzbine> DetaljiNarudzbine { get; set; }
        public ICollection<Magacin> Magacin { get; set; }
    }
}