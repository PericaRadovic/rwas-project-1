using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ParketarskaRadnja.Models
{
    public class ParketarskaRadnjaBaza : DbContext
    {
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Magacin> Magacini { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Narudzbine> Narudzbine { get;set; }
        public DbSet<DetaljiNarudzbine> DetaljiNarudzbine { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Otklanjanje 's' 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //relacije
            // Relacija: Proizvod - Magacin (jedan-na-više)
            modelBuilder.Entity<Magacin>()
                .HasRequired(m => m.Proizvod)
                .WithMany(p => p.Magacin)
                .HasForeignKey(m => m.ProizvodID);

            // Relacija: Klijent - Narudzbine (jedan-na-više)
            modelBuilder.Entity<Narudzbine>()
                .HasRequired(n => n.Klijent)
                .WithMany(k => k.Narudzbine)
                .HasForeignKey(n => n.KlijentID);

            // Relacija: Narudzbine - DetaljiNarudzbine (jedan-na-više)
            modelBuilder.Entity<DetaljiNarudzbine>()
                .HasRequired(dn => dn.Narudzbine)
                .WithMany(n => n.DetaljiNarudzbine)
                .HasForeignKey(dn => dn.NarudzbinaID);

            // Relacija: Proizvod - DetaljiNarudzbine (jedan-na-više)
            modelBuilder.Entity<DetaljiNarudzbine>()
                .HasRequired(dn => dn.Proizvod)
                .WithMany(p => p.DetaljiNarudzbine)
                .HasForeignKey(dn => dn.ProizvodID);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}