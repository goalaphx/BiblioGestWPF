using BiblioGest.Models; // Pour accéder aux classes d'entités (Livre, Adherent, etc.)
using Microsoft.EntityFrameworkCore;
using System; // Pour DateTime

namespace BiblioGest.Data // Assurez-vous que ce namespace correspond à votre projet/dossier
{
    public class BiblioGestContext : DbContext
    {
        // --- DbSet<TEntity> ---
        // Représente une collection (table) de chaque type d'entité dans le contexte.
        // EF Core utilisera ces propriétés pour interroger et sauvegarder les données.
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        // --- Constructeur ---
        // Ce constructeur est essentiel pour que l'injection de dépendances (configurée dans App.xaml.cs)
        // puisse passer les options de configuration (comme la chaîne de connexion) au DbContext.
        public BiblioGestContext(DbContextOptions<BiblioGestContext> options) : base(options)
        {
        }

        // --- OnModelCreating (Configuration via Fluent API) ---
        // Cette méthode est appelée par EF Core lors de la création initiale du modèle.
        // Elle permet de configurer des aspects du modèle qui ne peuvent pas être déduits
        // par convention ou via les Data Annotations (attributs) sur les entités.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Il est bon d'appeler la méthode de base d'abord
            base.OnModelCreating(modelBuilder);

            // === Configurations Spécifiques ===

            // -- Entité Livre --
            modelBuilder.Entity<Livre>(entity =>
            {
                // Définir un index unique sur la colonne ISBN pour s'assurer
                // qu'il n'y a pas deux livres avec le même ISBN.
                entity.HasIndex(l => l.ISBN)
                      .IsUnique();

                // Préciser la relation avec Categorie (même si EF peut la déduire)
                // Un Livre (l) a une (HasOne) Categorie...
                // Une Categorie peut avoir plusieurs (WithMany) Livres...
                // La clé étrangère dans Livre est (HasForeignKey) CategorieId.
                entity.HasOne(l => l.Categorie)
                      .WithMany(c => c.Livres)
                      .HasForeignKey(l => l.CategorieId)
                      // Optionnel: Définir le comportement en cas de suppression de la catégorie
                      // Restrict: Empêche de supprimer une catégorie si des livres y sont liés. (Comportement par défaut souvent)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // -- Entité Adherent --
            modelBuilder.Entity<Adherent>(entity =>
            {
                // Définir un index unique sur l'Email pour éviter les doublons
                entity.HasIndex(a => a.Email)
                      .IsUnique();
            });


            // -- Entité Emprunt --
            modelBuilder.Entity<Emprunt>(entity =>
            {
                // Préciser la relation Emprunt -> Livre
                entity.HasOne(e => e.Livre)
                      .WithMany(l => l.Emprunts) // Un livre peut avoir plusieurs emprunts (historique)
                      .HasForeignKey(e => e.LivreId)
                      .OnDelete(DeleteBehavior.Restrict); // Empêche de supprimer un livre s'il a des emprunts liés

                // Préciser la relation Emprunt -> Adherent
                entity.HasOne(e => e.Adherent)
                      .WithMany(a => a.Emprunts) // Un adhérent peut avoir plusieurs emprunts
                      .HasForeignKey(e => e.AdherentId)
                      .OnDelete(DeleteBehavior.Restrict); // Empêche de supprimer un adhérent s'il a des emprunts liés
            });


            // === Ajout de Données Initiales (Seed Data) ===
            // Ces données seront insérées dans la base lors de la création (ou migration).
            // Utile pour les données de base comme les catégories.

            modelBuilder.Entity<Categorie>().HasData(
                new Categorie { Id = 1, Nom = "Roman", Description = "Fiction narrative longue en prose." },
                new Categorie { Id = 2, Nom = "Science-Fiction", Description = "Genre basé sur des éléments scientifiques spéculatifs." },
                new Categorie { Id = 3, Nom = "Policier", Description = "Genre centré sur la résolution d'un crime." },
                new Categorie { Id = 4, Nom = "Histoire", Description = "Ouvrages traitant d'événements passés." },
                new Categorie { Id = 5, Nom = "Bande Dessinée", Description = "Art séquentiel combinant texte et images." },
                new Categorie { Id = 6, Nom = "Documentaire", Description = "Basé sur des faits réels, informatif." },
                new Categorie { Id = 7, Nom = "Jeunesse", Description = "Livres destinés aux enfants et adolescents." }
            );

            // Exemple pour ajouter un adhérent de test (décommentez si besoin)
            /*
            modelBuilder.Entity<Adherent>().HasData(
                new Adherent
                {
                    Id = 1,
                    Nom = "Admin",
                    Prenom = "Biblio",
                    Adresse = "1 rue de la Bibliothèque",
                    Email = "admin@bibliogest.local",
                    Telephone = "0102030405",
                    DateInscription = DateTime.Now.Date, // Prend la date du jour de la migration
                    Statut = StatutAdherent.Actif
                }
            );
            */
        }
    }
}