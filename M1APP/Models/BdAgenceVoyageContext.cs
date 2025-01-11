using System.Data.Entity;
namespace M1APP.Models
{
    public class BdAgenceVoyageContext:DbContext
    {
        public BdAgenceVoyageContext():base("connAgenceVoyage")
        { }
        public DbSet<Chauffeur> chauffeurs { get; set; }

        public DbSet<Utilisateur> utilisateurs { get; set; }

        public DbSet<Admin> admins { get; set; }

        public DbSet<Client> clients { get; set; }

        public DbSet<Gestionnaire> gestionnaires { get; set; }
            public DbSet<Flotte> flottes { get; set; }
        public DbSet<Offre> offres { get; set; }
        public DbSet<Reseervation> reservations { get; set; }

    }
}