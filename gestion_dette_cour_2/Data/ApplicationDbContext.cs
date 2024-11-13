using Microsoft.EntityFrameworkCore;
using gestion_dette_cour_2.Entities;

namespace gestion_dette_cour_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Chaîne de connexion directe dans la classe
        private const string ConnectionString = "server=localhost;port=8889;database=gestion_dette_csharp_web;user=root;password=root";

        // Constructeur qui utilise la chaîne de connexion directement
        // public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
            }
        }

        public DbSet<Client>? Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired();
                entity.Property(e => e.Prenom).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });
        }
    }
}
