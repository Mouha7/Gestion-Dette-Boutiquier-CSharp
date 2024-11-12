// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using gestion_dette_web.Models;

namespace gestion_dette_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration du modèle Client
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