namespace M1APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class premier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        IdUtilisateur = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(nullable: false, maxLength: 80),
                        PrenomUtilisateur = c.String(nullable: false, maxLength: 80),
                        EmailUtilisateur = c.String(nullable: false, maxLength: 80),
                        PasswordUtilisateur = c.String(),
                        TelUtilisateur = c.String(nullable: false, maxLength: 20),
                        idUserOwin = c.String(maxLength: 200),
                        MatriculeAdmin = c.String(maxLength: 20),
                        CNIGestionnaire = c.String(maxLength: 20),
                        CniClient = c.String(maxLength: 20),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdUtilisateur);
            
            CreateTable(
                "dbo.Agences",
                c => new
                    {
                        IdAgence = c.Int(nullable: false, identity: true),
                        AdresseAgence = c.String(nullable: false, maxLength: 150),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        NineaGestionnaire = c.String(nullable: false, maxLength: 20),
                        RccmGestionnaire = c.String(nullable: false, maxLength: 20),
                        IdGestionnaire = c.Int(),
                    })
                .PrimaryKey(t => t.IdAgence)
                .ForeignKey("dbo.Utilisateurs", t => t.IdGestionnaire)
                .Index(t => t.IdGestionnaire);
            
            CreateTable(
                "dbo.Offres",
                c => new
                    {
                        IdOffre = c.Int(nullable: false, identity: true),
                        DescriptionOffre = c.String(nullable: false, maxLength: 2000),
                        PrixOffre = c.Single(nullable: false),
                        DisponibiliteOffre = c.String(maxLength: 20),
                        IdAgence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOffre)
                .ForeignKey("dbo.Agences", t => t.IdAgence, cascadeDelete: true)
                .Index(t => t.IdAgence);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        IdVoyage = c.Int(nullable: false, identity: true),
                        Destination = c.String(nullable: false, maxLength: 100),
                        DateDepart = c.DateTime(nullable: false),
                        DateArrivee = c.DateTime(nullable: false),
                        Prix = c.Single(nullable: false),
                        Offre_IdOffre = c.Int(),
                    })
                .PrimaryKey(t => t.IdVoyage)
                .ForeignKey("dbo.Offres", t => t.Offre_IdOffre)
                .Index(t => t.Offre_IdOffre);
            
            CreateTable(
                "dbo.Chauffeurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 80),
                        Prenom = c.String(nullable: false, maxLength: 80),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voyages", "Offre_IdOffre", "dbo.Offres");
            DropForeignKey("dbo.Offres", "IdAgence", "dbo.Agences");
            DropForeignKey("dbo.Agences", "IdGestionnaire", "dbo.Utilisateurs");
            DropIndex("dbo.Voyages", new[] { "Offre_IdOffre" });
            DropIndex("dbo.Offres", new[] { "IdAgence" });
            DropIndex("dbo.Agences", new[] { "IdGestionnaire" });
            DropTable("dbo.Chauffeurs");
            DropTable("dbo.Voyages");
            DropTable("dbo.Offres");
            DropTable("dbo.Agences");
            DropTable("dbo.Utilisateurs");
        }
    }
}
