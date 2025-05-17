// Dans ViewModels/CategoryEditViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiblioGest.ViewModels
{
    public partial class CategoryEditViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;
        private Categorie _originalCategorie = new Categorie();

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Le nom de la catégorie est obligatoire.")]
        [MaxLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        private string? _nom;

        [ObservableProperty]
        [NotifyDataErrorInfo] // Optional: Add validation attributes if Description has rules
        [MaxLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        private string? _description;

        [ObservableProperty]
        private string _windowTitle = "Nouvelle Catégorie";

        [ObservableProperty]
        private bool _isEditMode = false;

        public CategoryEditViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }

        // Method called by MainViewModel to initialize data
        public void SetCategorie(Categorie? categorie)
        {
            ClearErrors(); // From ObservableValidator
            if (categorie == null) // Add Mode
            {
                IsEditMode = false;
                WindowTitle = "Nouvelle Catégorie";
                _originalCategorie = new Categorie(); // Fresh object
                Nom = string.Empty;
                Description = string.Empty;
            }
            else // Edit Mode
            {
                IsEditMode = true;
                WindowTitle = $"Modifier Catégorie : {categorie.Nom}";
                _originalCategorie = categorie; // Store original for comparison/ID
                Nom = categorie.Nom;
                Description = categorie.Description;
            }
            ValidateAllProperties(); // Trigger initial validation
        }

        public override Task LoadAsync()
        {
            // No additional data needs to be loaded for this simple form
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task SaveCategoryAsync()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                MessageBox.Show("Veuillez corriger les erreurs avant de sauvegarder.", "Erreurs de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsBusy) return;
            IsBusy = true;

            System.Diagnostics.Debug.WriteLine($"--- SaveCategoryAsync ({DateTime.Now}) ---");
            System.Diagnostics.Debug.WriteLine($"IsEditMode: {IsEditMode}, Nom: {Nom}");

            try
            {
                Categorie categoryToSave;

                // Check for duplicate category name (case-insensitive) before saving
                // This is a business rule check, distinct from database unique constraint
                string currentNameLower = Nom!.ToLower();
                bool nameExists;
                if (IsEditMode)
                {
                    // When editing, check if another category (not the current one) has the same name
                    nameExists = await _context.Categories
                                             .AnyAsync(c => c.Id != _originalCategorie.Id && c.Nom.ToLower() == currentNameLower);
                }
                else
                {
                    // When adding, check if any category has the same name
                    nameExists = await _context.Categories.AnyAsync(c => c.Nom.ToLower() == currentNameLower);
                }

                if (nameExists)
                {
                    MessageBox.Show($"Une catégorie avec le nom '{Nom}' existe déjà. Veuillez choisir un nom différent.", "Nom Dupliqué", MessageBoxButton.OK, MessageBoxImage.Warning);
                    // Optionally, add error to Nom property via INotifyDataErrorInfo if desired
                    // SetErrors(nameof(Nom), new List<string> { "Ce nom de catégorie existe déjà." });
                    IsBusy = false;
                    return;
                }
                // ClearErrors(nameof(Nom)); // Clear if previously set

                if (IsEditMode)
                {
                    categoryToSave = await _context.Categories.FindAsync(_originalCategorie.Id);
                    if (categoryToSave == null)
                    {
                        MessageBox.Show($"Erreur: La catégorie ID '{_originalCategorie.Id}' n'a pas été trouvée.", "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsBusy = false;
                        return;
                    }
                }
                else // Add new category
                {
                    categoryToSave = new Categorie();
                    _context.Categories.Add(categoryToSave);
                }

                categoryToSave.Nom = Nom!; // Nom is not null due to [Required]
                categoryToSave.Description = Description ?? string.Empty; // Handle nullable Description

                System.Diagnostics.Debug.WriteLine($"ChangeTracker (Categories) before SaveChanges: Added={_context.ChangeTracker.Entries<Categorie>().Count(e=>e.State == EntityState.Added)}, Modified={_context.ChangeTracker.Entries<Categorie>().Count(e=>e.State == EntityState.Modified)}");
                int changes = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync (Categories) completed. Changes: {changes}");


                if (changes > 0 || (IsEditMode && !NameChangedFromOriginal(_originalCategorie, categoryToSave) && !DescriptionChangedFromOriginal(_originalCategorie, categoryToSave))) // Handle no actual change in edit mode
                {
                    MessageBox.Show("Catégorie sauvegardée avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _mainViewModel.RequestReturnToCategoryList();
                }
                else
                {
                     MessageBox.Show("Aucune modification n'a été sauvegardée.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                     // Optionally navigate back or stay on page
                     await _mainViewModel.RequestReturnToCategoryList();
                }
            }
            catch (DbUpdateException dbEx) // Catches database-level errors (e.g., unique constraint if not caught above)
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateException (Category): {dbEx.ToString()}");
                string specificError = "Erreur base de données.";
                 if (dbEx.InnerException?.Message.ToUpper().Contains("IX_CATEGORIES_NOM") ?? false) // Adjust if your constraint name is different
                 {
                     specificError = "Ce nom de catégorie existe déjà (contrainte base de données).";
                 }
                HandleError($"sauvegarde catégorie ({specificError})", dbEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Exception (Category): {ex.ToString()}");
                HandleError("sauvegarde de la catégorie", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        // Helper methods to check if properties actually changed (for edit mode success message)
        private bool NameChangedFromOriginal(Categorie original, Categorie current) => original.Nom != current.Nom;
        private bool DescriptionChangedFromOriginal(Categorie original, Categorie current) => (original.Description ?? string.Empty) != (current.Description ?? string.Empty);


        [RelayCommand]
        private async Task CancelEdit()
        {
            await _mainViewModel.RequestReturnToCategoryList();
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null) message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}