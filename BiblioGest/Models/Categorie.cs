using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Pour les attributs comme [Required]

namespace BiblioGest.Models // Assurez-vous que ce namespace correspond à votre projet/dossier
{
    public class Categorie
    {
        public int Id { get; set; } // Clé primaire (par convention EF Core)

        [Required(ErrorMessage = "Le nom de la catégorie est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Nom { get; set; } = string.Empty; // Initialisation pour éviter les avertissements null

        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string? Description { get; set; } // Nullable : la description est optionnelle

        // Propriété de navigation: Une catégorie peut contenir plusieurs livres
        // 'virtual' est utile si vous activez le Lazy Loading plus tard
        public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
    }
}