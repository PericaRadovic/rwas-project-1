namespace ParketarskaRadnja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetaljiNarudzbine",
                c => new
                    {
                        DetaljNarudzbineID = c.Int(nullable: false, identity: true),
                        NarudzbinaID = c.Int(nullable: false),
                        ProizvodID = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        CenaPoJedinici = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DetaljNarudzbineID)
                .ForeignKey("dbo.Narudzbine", t => t.NarudzbinaID, cascadeDelete: true)
                .ForeignKey("dbo.Proizvod", t => t.ProizvodID, cascadeDelete: true)
                .Index(t => t.NarudzbinaID)
                .Index(t => t.ProizvodID);
            
            CreateTable(
                "dbo.Narudzbine",
                c => new
                    {
                        NarudzbinaID = c.Int(nullable: false, identity: true),
                        KlijentID = c.Int(nullable: false),
                        DatumNarudzbine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NarudzbinaID)
                .ForeignKey("dbo.Klijent", t => t.KlijentID, cascadeDelete: true)
                .Index(t => t.KlijentID);
            
            CreateTable(
                "dbo.Klijent",
                c => new
                    {
                        KlijentID = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Adresa = c.String(),
                        BrojTelefona = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.KlijentID);
            
            CreateTable(
                "dbo.Proizvod",
                c => new
                    {
                        ProizvodID = c.Int(nullable: false, identity: true),
                        NazivProizvoda = c.String(),
                        Opis = c.String(),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProizvodID);
            
            CreateTable(
                "dbo.Magacin",
                c => new
                    {
                        StanjeID = c.Int(nullable: false, identity: true),
                        ProizvodID = c.Int(nullable: false),
                        KolicinaNaZalihi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StanjeID)
                .ForeignKey("dbo.Proizvod", t => t.ProizvodID, cascadeDelete: true)
                .Index(t => t.ProizvodID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetaljiNarudzbine", "ProizvodID", "dbo.Proizvod");
            DropForeignKey("dbo.Magacin", "ProizvodID", "dbo.Proizvod");
            DropForeignKey("dbo.DetaljiNarudzbine", "NarudzbinaID", "dbo.Narudzbine");
            DropForeignKey("dbo.Narudzbine", "KlijentID", "dbo.Klijent");
            DropIndex("dbo.Magacin", new[] { "ProizvodID" });
            DropIndex("dbo.Narudzbine", new[] { "KlijentID" });
            DropIndex("dbo.DetaljiNarudzbine", new[] { "ProizvodID" });
            DropIndex("dbo.DetaljiNarudzbine", new[] { "NarudzbinaID" });
            DropTable("dbo.Magacin");
            DropTable("dbo.Proizvod");
            DropTable("dbo.Klijent");
            DropTable("dbo.Narudzbine");
            DropTable("dbo.DetaljiNarudzbine");
        }
    }
}
