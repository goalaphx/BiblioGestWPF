using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioGest.Models
{
    public class Emprunt
    {
        public int Id { get; set; } // Clé primaire

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date d'emprunt")]
        public DateTime DateEmprunt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Retour Prévu")]
        public DateTime DateRetourPrevue { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Retour Effectif")]
        public DateTime? DateRetourEffective { get; set; } // Nullable: Le livre peut ne pas être encore retourné

        // Clé étrangère vers Livre
        [Required]
        public int LivreId { get; set; }

        // Clé étrangère vers Adherent
        [Required]
        public int AdherentId { get; set; }

        // Propriété de navigation vers le Livre emprunté
        [ForeignKey("LivreId")]
        public virtual Livre Livre { get; set; } = null!;

        // Propriété de navigation vers l'Adherent qui a emprunté
        [ForeignKey("AdherentId")]
        public virtual Adherent Adherent { get; set; } = null!;

        // Propriété calculée (non mappée) pour savoir si l'emprunt est en retard
        [NotMapped]
        public bool EstEnRetard => DateRetourEffective == null && DateRetourPrevue < DateTime.Today;
    }
}