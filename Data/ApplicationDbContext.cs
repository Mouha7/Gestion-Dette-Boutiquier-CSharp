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

        // Define your DbSet properties here.
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dette> Dettes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Relation entre User et Client
            modelBuilder.Entity<Client>()
            .HasMany(c => c.Dettes)
            .WithOne(d => d.Client)
            .HasForeignKey(d => d.ClientId);
            // Relation entre User et Client
            modelBuilder.Entity<User>()
            .HasOne(user => user.Client)
            .WithOne(client => client.User)
            .HasForeignKey<Client>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            // Configuration du modèle Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Surnom).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });
        }
    }
}