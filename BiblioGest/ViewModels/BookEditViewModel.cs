// Dans ViewModels/BookEditViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiblioGest.ViewModels
{
    public partial class BookEditViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;
        private Livre _originalBook = new Livre();

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "L'ISBN est obligatoire.")]
        [MaxLength(20, ErrorMessage = "L'ISBN ne doit pas dépasser 20 caractères.")]
        private string? _isbn;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        [MaxLength(250, ErrorMessage = "Le titre ne doit pas dépasser 250 caractères.")]
        private string? _titre;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "L'auteur est obligatoire.")]
        [MaxLength(200, ErrorMessage = "L'auteur ne doit pas dépasser 200 caractères.")]
        private string? _auteur;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MaxLength(150, ErrorMessage = "L'éditeur ne doit pas dépasser 150 caractères.")]
        private string? _editeur;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(1000, 9999, ErrorMessage = "Année invalide (doit être entre 1000 et 9999).")]
        private int _anneePublication;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0, int.MaxValue, ErrorMessage = "Le nombre total d'exemplaires doit être positif ou nul.")]
        private int _nombreExemplairesTotal;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0, int.MaxValue, ErrorMessage = "Le nombre d'exemplaires disponibles doit être positif ou nul.")]
        [NotifyPropertyChangedFor(nameof(CanDecreaseAvailable))]
        private int _nombreExemplairesDisponibles;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "La catégorie est obligatoire.")] // This attribute handles the null check
        private Categorie? _selectedCategorie;

        [ObservableProperty]
        private ObservableCollection<Categorie> _categories = new();

        [ObservableProperty]
        private string _windowTitle = "Nouveau Livre";

        [ObservableProperty]
        private bool _isEditMode = false;

        public bool CanDecreaseAvailable => NombreExemplairesDisponibles > 0;

        public BookEditViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            AnneePublication = DateTime.Now.Year;
        }

        public void SetLivre(Livre? livre)
        {
            ClearErrors();
            if (livre == null)
            {
                IsEditMode = false;
                WindowTitle = "Nouveau Livre";
                _originalBook = new Livre();
                Isbn = string.Empty;
                Titre = string.Empty;
                Auteur = string.Empty;
                Editeur = string.Empty;
                AnneePublication = DateTime.Now.Year;
                NombreExemplairesTotal = 1;
                NombreExemplairesDisponibles = 1;
                SelectedCategorie = null;
            }
            else
            {
                IsEditMode = true;
                WindowTitle = $"Modifier : {livre.Titre}";
                _originalBook = livre;
                Isbn = livre.ISBN;
                Titre = livre.Titre;
                Auteur = livre.Auteur;
                Editeur = livre.Editeur;
                AnneePublication = livre.AnneePublication;
                NombreExemplairesTotal = livre.NombreExemplairesTotal;
                NombreExemplairesDisponibles = livre.NombreExemplairesDisponibles;
                if (Categories.Any())
                {
                    SelectedCategorie = Categories.FirstOrDefault(c => c.Id == livre.CategorieId);
                }
                else
                {
                    SelectedCategorie = null;
                }
            }
            ValidateAllProperties();
        }

        public override async Task LoadAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            Categories.Clear();
            try
            {
                var catsFromDb = await _context.Categories.OrderBy(c => c.Nom).ToListAsync();
                foreach (var cat in catsFromDb)
                {
                    Categories.Add(cat);
                }
                if (IsEditMode && _originalBook != null)
                {
                    SelectedCategorie = Categories.FirstOrDefault(c => c.Id == _originalBook.CategorieId);
                }
                else if (!IsEditMode && SelectedCategorie == null && Categories.Any())
                {
                    SelectedCategorie = Categories.First();
                }
            }
            catch (Exception ex)
            {
                HandleError("chargement des catégories", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SaveBookAsync()
        {
            ValidateAllProperties(); // Driven by DataAnnotations
            // The [Required] attribute on SelectedCategorie should handle the null check.
            // If it passes, SelectedCategorie will not be null.

            if (HasErrors)
            {
                MessageBox.Show("Veuillez corriger les erreurs indiquées avant de sauvegarder.", "Erreurs de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Specific logical check for copy counts
            if (NombreExemplairesDisponibles > NombreExemplairesTotal)
            {
                MessageBox.Show("Le nombre d'exemplaires disponibles ne peut pas être supérieur au nombre total.", "Erreur Logique", MessageBoxButton.OK, MessageBoxImage.Warning);
                // REMOVED: SetErrors(nameof(NombreExemplairesDisponibles), new List<string> { "Ne peut pas être supérieur au nombre total." });
                return; // Stop execution if this logical error occurs
            }
            // REMOVED: else { ClearErrors(nameof(NombreExemplairesDisponibles)); }


            if (IsBusy) return;
            IsBusy = true;

            System.Diagnostics.Debug.WriteLine($"--- SaveBookAsync ({DateTime.Now}) ---");
            System.Diagnostics.Debug.WriteLine($"IsEditMode: {IsEditMode}");
            System.Diagnostics.Debug.WriteLine($"Context HashCode at start of Save: {_context.GetHashCode()}");
            System.Diagnostics.Debug.WriteLine($"ISBN: {Isbn}, Titre: {Titre}, CategorieID from ViewModel: {SelectedCategorie?.Id}"); // SelectedCategorie should not be null here if [Required] worked

            try
            {
                Livre bookToSave;

                if (IsEditMode)
                {
                    System.Diagnostics.Debug.WriteLine($"Editing Book with ID: {_originalBook.Id}");
                    bookToSave = await _context.Livres.FindAsync(_originalBook.Id);
                    if (bookToSave == null)
                    {
                        MessageBox.Show($"Erreur: Le livre avec l'ID '{_originalBook.Id}' n'a pas été trouvé pour la mise à jour.", "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsBusy = false;
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine($"Found Book for edit. Current DB Title: {bookToSave.Titre}");
                }
                else // Add new book
                {
                    bookToSave = new Livre();
                    System.Diagnostics.Debug.WriteLine($"New Book created. Entity State BEFORE Add: {_context.Entry(bookToSave).State}");
                    _context.Livres.Add(bookToSave);
                    System.Diagnostics.Debug.WriteLine($"New Book added to context. Entity State AFTER Add: {_context.Entry(bookToSave).State}");
                }

                bookToSave.ISBN = this.Isbn!;
                bookToSave.Titre = this.Titre!;
                bookToSave.Auteur = this.Auteur!;
                bookToSave.Editeur = this.Editeur ?? string.Empty;
                bookToSave.AnneePublication = this.AnneePublication;
                bookToSave.NombreExemplairesTotal = this.NombreExemplairesTotal;
                bookToSave.NombreExemplairesDisponibles = this.NombreExemplairesDisponibles;
                bookToSave.CategorieId = this.SelectedCategorie!.Id; // If [Required] passed, SelectedCategorie is not null.

                System.Diagnostics.Debug.WriteLine($"Entity to save: ISBN='{bookToSave.ISBN}', Titre='{bookToSave.Titre}', CategorieId='{bookToSave.CategorieId}'");

                _context.ChangeTracker.DetectChanges();
                var addedEntries = _context.ChangeTracker.Entries<Livre>().Where(e => e.State == EntityState.Added).ToList();
                var modifiedEntries = _context.ChangeTracker.Entries<Livre>().Where(e => e.State == EntityState.Modified).ToList();
                System.Diagnostics.Debug.WriteLine($"ChangeTracker: Added Livres={addedEntries.Count}, Modified Livres={modifiedEntries.Count}");
                if (addedEntries.Any()) System.Diagnostics.Debug.WriteLine($"ChangeTracker: First Added Livre (Titre): {addedEntries.First().Entity.Titre}");

                System.Diagnostics.Debug.WriteLine($"Context HashCode BEFORE SaveChanges: {_context.GetHashCode()}");
                int changes = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync completed. Number of state entries written to database: {changes}");

                if (changes > 0)
                {
                    MessageBox.Show("Livre sauvegardé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _mainViewModel.RequestReturnToBookList();
                }
                else
                {
                    MessageBox.Show("Aucune modification n'a été sauvegardée dans la base de données. Vérifiez les données.", "Avertissement Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Warning);
                    System.Diagnostics.Debug.WriteLine("WARN: SaveChangesAsync reported 0 changes.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx.ToString()}");
                string specificError = "Une erreur de base de données est survenue lors de la sauvegarde.";
                if (dbEx.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"DbUpdateException Inner: {dbEx.InnerException.ToString()}");
                    if (dbEx.InnerException.Message.ToUpper().Contains("IX_LIVRES_ISBN") || (dbEx.InnerException.Message.ToUpper().Contains("UNIQUE CONSTRAINT") && dbEx.InnerException.Message.ToUpper().Contains("ISBN")))
                    {
                        specificError = "Cet ISBN existe déjà. Veuillez en utiliser un autre.";
                    }
                    else if (dbEx.InnerException.Message.ToUpper().Contains("FOREIGN KEY CONSTRAINT") && dbEx.InnerException.Message.ToUpper().Contains("CATEGORIEID"))
                    {
                         specificError = "La catégorie sélectionnée n'est pas valide ou n'existe plus.";
                    }
                     else if (dbEx.InnerException.Message.ToUpper().Contains("NOT-NULL CONSTRAINT") && dbEx.InnerException.Message.ToUpper().Contains("CATEGORIEID")) // Example
                    {
                         specificError = "La catégorie est obligatoire et n'a pas été fournie.";
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
                HandleError("sauvegarde du livre", ex);
            }
            finally
            {
                IsBusy = false;
                System.Diagnostics.Debug.WriteLine($"SaveBookAsync finally block executed ({DateTime.Now}).");
            }
        }

        [RelayCommand]
        private async Task CancelEdit()
        {
            await _mainViewModel.RequestReturnToBookList();
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