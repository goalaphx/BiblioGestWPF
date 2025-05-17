// Dans ViewModels/MemberEditViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiblioGest.ViewModels
{
    public partial class MemberEditViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;
        private Adherent _originalAdherent = new Adherent();

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
        private string? _nom;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le prénom ne doit pas dépasser 100 caractères.")]
        private string? _prenom;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        [MaxLength(300, ErrorMessage = "L'adresse ne doit pas dépasser 300 caractères.")]
        private string? _adresse;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "L'email est obligatoire.")]
        [MaxLength(150, ErrorMessage = "L'email ne doit pas dépasser 150 caractères.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        private string? _email;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MaxLength(20, ErrorMessage = "Le téléphone ne doit pas dépasser 20 caractères.")]
        [Phone(ErrorMessage = "Format de téléphone invalide.")]
        private string? _telephone;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Le statut est obligatoire.")]
        private StatutAdherent _statut;

        public List<StatutAdherent> StatutsPossibles { get; } = Enum.GetValues(typeof(StatutAdherent)).Cast<StatutAdherent>().ToList();

        [ObservableProperty]
        private string _windowTitle = "Nouvel Adhérent";

        [ObservableProperty]
        private bool _isEditMode = false;

        private DateTime _dateInscription; // This will store the date, ideally as UTC for new entries

        // For display, you might want to convert to local time if _dateInscription is UTC
        // Or, ensure _dateInscription is always what you want to display.
        // For simplicity, this directly reflects _dateInscription.
        public DateTime DateInscriptionDisplay => _dateInscription;

        public MemberEditViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }

        public void SetAdherent(Adherent? adherent)
        {
            ClearErrors();
            if (adherent == null) // Add Mode
            {
                IsEditMode = false;
                WindowTitle = "Nouvel Adhérent";

                // Initialize _dateInscription as UTC midnight for new adherents
                _dateInscription = DateTime.UtcNow.Date; // Ensures Kind is Utc and it's just the date part

                _originalAdherent = new Adherent { DateInscription = _dateInscription }; // Store this UTC date

                Nom = string.Empty;
                Prenom = string.Empty;
                Adresse = string.Empty;
                Email = string.Empty;
                Telephone = string.Empty;
                Statut = StatutAdherent.Actif;
            }
            else // Edit Mode
            {
                IsEditMode = true;
                WindowTitle = $"Modifier : {adherent.NomComplet}";
                _originalAdherent = adherent;

                Nom = adherent.Nom;
                Prenom = adherent.Prenom;
                Adresse = adherent.Adresse;
                Email = adherent.Email;
                Telephone = adherent.Telephone;
                Statut = adherent.Statut;

                // When loading from DB for edit, ensure the Kind is UTC if it's not already.
                // Npgsql + EF Core typically handles this well for timestamptz,
                // but explicitly setting Kind can prevent issues if it's Unspecified or Local.
                _dateInscription = DateTime.SpecifyKind(adherent.DateInscription, DateTimeKind.Utc);
            }
            OnPropertyChanged(nameof(DateInscriptionDisplay));
            ValidateAllProperties();
        }

        public override Task LoadAsync()
        {
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SaveMemberAsync()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                MessageBox.Show("Veuillez corriger les erreurs indiquées avant de sauvegarder.", "Erreurs de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsBusy) return;
            IsBusy = true;

            System.Diagnostics.Debug.WriteLine($"--- SaveMemberAsync ({DateTime.Now}) ---");
            System.Diagnostics.Debug.WriteLine($"IsEditMode: {IsEditMode}");
            System.Diagnostics.Debug.WriteLine($"Context HashCode at start of Save: {_context.GetHashCode()}");
            System.Diagnostics.Debug.WriteLine($"Nom: {Nom}, Prenom: {Prenom}, Email: {Email}, Statut: {Statut}, ViewModel _dateInscription: {_dateInscription:O} (Kind: {_dateInscription.Kind})");


            try
            {
                Adherent memberToSave;

                if (IsEditMode)
                {
                    System.Diagnostics.Debug.WriteLine($"Editing Adherent with ID: {_originalAdherent.Id}");
                    memberToSave = await _context.Adherents.FindAsync(_originalAdherent.Id);
                    if (memberToSave == null)
                    {
                        MessageBox.Show($"Erreur: L'adhérent avec l'ID '{_originalAdherent.Id}' n'a pas été trouvé pour la mise à jour.", "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsBusy = false;
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine($"Found Adherent for edit. Current DB Email: {memberToSave.Email}");
                    // DateInscription is NOT updated during edit for this application's logic.
                    // If it were, we'd ensure its Kind is Utc:
                    // memberToSave.DateInscription = DateTime.SpecifyKind(this._dateInscription, DateTimeKind.Utc);
                }
                else // Add new member
                {
                    memberToSave = new Adherent
                    {
                        // _dateInscription from ViewModel should already be UTC
                        DateInscription = DateTime.SpecifyKind(this._dateInscription, DateTimeKind.Utc)
                    };
                    System.Diagnostics.Debug.WriteLine($"New Adherent created. DateInscription to be saved: {memberToSave.DateInscription:O} (Kind: {memberToSave.DateInscription.Kind})");
                    System.Diagnostics.Debug.WriteLine($"New Adherent created. Entity State BEFORE Add: {_context.Entry(memberToSave).State}");
                    _context.Adherents.Add(memberToSave);
                    System.Diagnostics.Debug.WriteLine($"New Adherent added to context. Entity State AFTER Add: {_context.Entry(memberToSave).State}");
                }

                memberToSave.Nom = this.Nom!;
                memberToSave.Prenom = this.Prenom!;
                memberToSave.Adresse = this.Adresse!;
                memberToSave.Email = this.Email!;
                memberToSave.Telephone = this.Telephone ?? string.Empty;
                memberToSave.Statut = this.Statut;

                System.Diagnostics.Debug.WriteLine($"Entity to save (final props): Nom='{memberToSave.Nom}', Email='{memberToSave.Email}', DateInscription='{memberToSave.DateInscription:O}', Statut='{memberToSave.Statut}'");

                _context.ChangeTracker.DetectChanges();
                var addedEntries = _context.ChangeTracker.Entries<Adherent>().Where(e => e.State == EntityState.Added).ToList();
                var modifiedEntries = _context.ChangeTracker.Entries<Adherent>().Where(e => e.State == EntityState.Modified).ToList();
                System.Diagnostics.Debug.WriteLine($"ChangeTracker: Added={addedEntries.Count}, Modified={modifiedEntries.Count}");
                if (addedEntries.Any()) System.Diagnostics.Debug.WriteLine($"ChangeTracker: First Added Adherent Preview (Email): {addedEntries.First().Entity.Email}");


                System.Diagnostics.Debug.WriteLine($"Context HashCode BEFORE SaveChanges: {_context.GetHashCode()}");
                int changes = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync completed. Number of state entries written to database: {changes}");

                if (changes > 0)
                {
                    MessageBox.Show("Adhérent sauvegardé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _mainViewModel.RequestReturnToMemberList();
                }
                else
                {
                    MessageBox.Show("Aucune modification n'a été sauvegardée dans la base de données. Vérifiez si des données ont été modifiées ou si l'entité était correctement suivie.", "Avertissement Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Warning);
                    System.Diagnostics.Debug.WriteLine("WARN: SaveChangesAsync reported 0 changes. Entity might not have been modified or tracked as Added.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx.ToString()}");
                string specificError = "Une erreur de base de données est survenue lors de la sauvegarde.";
                if (dbEx.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"DbUpdateException Inner: {dbEx.InnerException.ToString()}");
                    if (dbEx.InnerException.Message.Contains("IX_Adherents_Email"))
                    {
                        specificError = "Cet email est déjà utilisé. Veuillez en choisir un autre.";
                    }
                    else
                    {
                        specificError = $"Erreur BD: {dbEx.InnerException.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()}";
                    }
                }
                HandleError($"sauvegarde ({specificError})", dbEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Exception: {ex.ToString()}");
                HandleError("sauvegarde de l'adhérent", ex);
            }
            finally
            {
                IsBusy = false;
                System.Diagnostics.Debug.WriteLine($"SaveMemberAsync finally block executed ({DateTime.Now}).");
            }
        }

        [RelayCommand]
        private async Task CancelEdit()
        {
            await _mainViewModel.RequestReturnToMemberList();
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex is DbUpdateException dbEx && dbEx.InnerException != null)
            {
                message += $"\nDétails BD: {dbEx.InnerException.Message}";
            }
            else if (ex.InnerException != null)
            {
                message += $"\nDétails: {ex.InnerException.Message}";
            }
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}