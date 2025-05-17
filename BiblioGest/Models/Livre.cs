using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Pour [ForeignKey]

namespace BiblioGest.Models
{
    public class Livre
    {
        public int Id { get; set; } // Clé primaire

        [Required(ErrorMessage = "L'ISBN est obligatoire.")]
        [MaxLength(20, ErrorMessage = "L'ISBN ne peut pas dépasser 20 caractères.")] // ISBN-13 ou ISBN-10
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le titre est obligatoire.")]
        [MaxLength(250, ErrorMessage = "Le titre ne peut pas dépasser 250 caractères.")]
        public string Titre { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est obligatoire.")]
        [MaxLength(200, ErrorMessage = "Le nom de l'auteur ne peut pas dépasser 200 caractères.")]
        public string Auteur { get; set; } = string.Empty;

        [MaxLength(150, ErrorMessage = "Le nom de l'éditeur ne peut pas dépasser 150 caractères.")]
        public string Editeur { get; set; } = string.Empty;

        [Range(1000, 9999, ErrorMessage = "L'année de publication doit être valide.")] // Ajustez si nécessaire
        public int AnneePublication { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre total d'exemplaires doit être positif ou nul.")]
        [Display(Name = "Exemplaires Total")] // Nom affiché dans certaines UI auto-générées
        public int NombreExemplairesTotal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre d'exemplaires disponibles doit être positif ou nul.")]
        [Display(Name = "Exemplaires Disponibles")]
        public int NombreExemplairesDisponibles { get; set; } // Sera mis à jour lors des emprunts/retours

        // Clé étrangère vers la table Categorie
        [Required(ErrorMessage = "La catégorie est obligatoire.")]
        public int CategorieId { get; set; }

        // Propriété de navigation vers la Catégorie associée
        [ForeignKey("CategorieId")] // Lie explicitement la navigation à la clé étrangère
        public virtual Categorie Categorie { get; set; } = null!; // null! indique au compilateur que ce ne sera pas null après chargement par EF

        // Propriété de navigation: Un livre peut être dans plusieurs emprunts au fil du temps
        public virtual ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
    }
}