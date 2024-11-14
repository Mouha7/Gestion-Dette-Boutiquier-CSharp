using Microsoft.EntityFrameworkCore;
using gestion_dette_web.Models;

namespace gestion_dette_web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Dettes)
                .WithOne(d => d.Client)
                .HasForeignKey(d => d.ClientId);  // Associer la clé étrangère ClientId dans la table Dettes

            modelBuilder.Entity<User>()
                .HasOne(user => user.Client)
                .WithOne(client => client.User)
                .HasForeignKey<Client>(c => c.UserId)  // Associer la clé étrangère UserId dans la table Clients
                .OnDelete(DeleteBehavior.Cascade) // Supprimer un Client supprime l'User associé
                .IsRequired(false);  // La colonne UserId dans la table Clients n'est pas obligatoire

            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.Dette)
                .WithMany(d => d.Paiements)
                .HasForeignKey(p => p.DetteId);  // Associer la clé étrangère DetteId dans la table Paiements

            // Association many-to-many entre Dette et Article via Detail
            modelBuilder.Entity<Detail>()
                .HasKey(d => new { d.DetteId, d.ArticleId }); // Clé composite pour lier Dette et Article

            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Dette)
                .WithMany(d => d.Details)
                .HasForeignKey(d => d.DetteId);

            modelBuilder.Entity<Detail>()
                .HasOne(d => d.Article)
                .WithMany(a => a.Details)
                .HasForeignKey(d => d.ArticleId);

            // Définition des noms de tables (facultatif)
            modelBuilder.Entity<Client>().ToTable("clients");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Dette>().ToTable("dettes");
            modelBuilder.Entity<Detail>().ToTable("details");
            modelBuilder.Entity<Article>().ToTable("articles");
            modelBuilder.Entity<Paiement>().ToTable("paiements");
        }

        // DbSet pour chaque modèle
        public DbSet<Article> Articles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dette> Dettes { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Paiement> Paiements { get; set; }
    }
}
