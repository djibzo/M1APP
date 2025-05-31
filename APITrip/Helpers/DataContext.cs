using APITrip.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace APITrip.Helpers
{
    public class DataContext:DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var connectionString = Configuration.GetConnectionString("TestDb");
                options.UseNpgsql(connectionString);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Agence> Agences { get; set; }
        public DbSet<Chauffeur> Chauffeurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Flotte> Flottes { get; set; }
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Voyage> Voyages { get; set; }
    }
}
