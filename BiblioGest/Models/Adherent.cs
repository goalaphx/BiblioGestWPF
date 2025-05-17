using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioGest.Models
{
    // Énumération pour définir les statuts possibles d'un adhérent
    public enum StatutAdherent
    {
        [Display(Name = "Actif")] // Texte affiché
        Actif = 0,
        [Display(Name = "Suspendu")]
        Suspendu = 1,
        [Display(Name = "Expiré")]
        Expire = 2
    }

    public class Adherent
    {
        public int Id { get; set; } // Clé primaire

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MaxLength(100)]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        [MaxLength(300)]
        public string Adresse { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [MaxLength(150)]
        [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide.")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)] // Spécifie que seule la date est pertinente
        [Display(Name = "Date d'inscription")]
        public DateTime DateInscription { get; set; }

        [Required]
        public StatutAdherent Statut { get; set; } = StatutAdherent.Actif; // Valeur par défaut

        // Propriété de navigation: Un adhérent peut avoir plusieurs emprunts
        public virtual ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();

        // Propriété calculée (non mappée à la base de données) pour l'affichage facile
        [NotMapped] // Important: Indique à EF Core de ne pas créer de colonne pour cette propriété
        public string NomComplet => $"{Prenom} {Nom}";
    }
}