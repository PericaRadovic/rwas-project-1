using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParketarskaRadnja.Models
{
    public class Narudzbine
    {
        [Key]
        public int NarudzbinaID { get; set; }
        public int KlijentID { get; set; }
        public DateTime DatumNarudzbine { get; set; }

        public Klijent Klijent { get; set; }
        public ICollection<DetaljiNarudzbine> DetaljiNarudzbine { get; set; }
    }
}