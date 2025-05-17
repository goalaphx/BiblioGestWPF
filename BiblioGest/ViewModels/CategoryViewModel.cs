// Dans ViewModels/CategoryListViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;     // For Timer
using System.Threading.Tasks;
using System.Windows;     // For Application.Current.Dispatcher

namespace BiblioGest.ViewModels
{
    public partial class CategoryListViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private ObservableCollection<Categorie> _categories = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditCategoryCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCategoryCommand))]
        private Categorie? _selectedCategorie;

        // --- NEW: Search Functionality ---
        [ObservableProperty]
        private string? _searchText;

        private Timer? _searchDebounceTimer;
        private const int SearchDebounceTimeMs = 500;

        public CategoryListViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }

        // --- NEW: Called when SearchText changes ---
        async partial void OnSearchTextChanged(string? value)
        {
            _searchDebounceTimer?.Dispose();
            _searchDebounceTimer = new Timer(async _ =>
            {
                await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    await LoadCategoriesAsync(null);
                });
            }, null, SearchDebounceTimeMs, Timeout.Infinite);
        }

        public override async Task LoadAsync()
        {
            await LoadCategoriesAsync(null); // Initial load
        }

        // Modified to accept parameter
        [RelayCommand]
        private async Task LoadCategoriesAsync(string? commandParameter)
        {
            bool justClearedSearch = false;
            if ("clear_search".Equals(commandParameter))
            {
                if(!string.IsNullOrEmpty(SearchText))
                {
                    SearchText = string.Empty;
                    justClearedSearch = true;
                }
            }
            if (justClearedSearch && _searchDebounceTimer != null) return;


            if (IsBusy) return;
            IsBusy = true;
            Categories.Clear();
            SelectedCategorie = null;
            try
            {
                IQueryable<Categorie> query = _context.Categories.AsQueryable();

                // --- NEW: Apply Search Filter ---
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    string searchTextLower = SearchText.ToLower().Trim();
                    query = query.Where(c =>
                        (c.Nom != null && c.Nom.ToLower().Contains(searchTextLower)) ||
                        (c.Description != null && c.Description.ToLower().Contains(searchTextLower))
                    );
                }

                var categoriesFromDb = await query.OrderBy(c => c.Nom).ToListAsync();
                foreach (var category in categoriesFromDb)
                {
                    Categories.Add(category);
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
        private async Task ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText)) SearchText = string.Empty;
        }

        // AddCategory, EditCategory, DeleteCategoryAsync, CanEditOrDeleteCategory, HandleError remain the same
        [RelayCommand]
        private async Task AddCategory()
        {
            await _mainViewModel.NavigateToCategoryEdit(null);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteCategory))]
        private async Task EditCategory()
        {
            if (SelectedCategorie == null) return;
            await _mainViewModel.NavigateToCategoryEdit(SelectedCategorie);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteCategory))]
        private async Task DeleteCategoryAsync()
        {
            if (SelectedCategorie == null) return;
            bool isInUse = await _context.Livres.AnyAsync(l => l.CategorieId == SelectedCategorie.Id);
            if (isInUse)
            {
                MessageBox.Show($"La catégorie '{SelectedCategorie.Nom}' est utilisée par un ou plusieurs livres et ne peut pas être supprimée.",
                                "Suppression Impossible", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show($"Supprimer la catégorie '{SelectedCategorie.Nom}' ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var categoryToDelete = await _context.Categories.FindAsync(SelectedCategorie.Id);
                if (categoryToDelete != null)
                {
                    _context.Categories.Remove(categoryToDelete);
                    await _context.SaveChangesAsync();
                    Categories.Remove(SelectedCategorie);
                    SelectedCategorie = null;
                }
            }
            catch (DbUpdateException dbEx)
            {
                HandleError("suppression de la catégorie (vérifiez les livres associés)", dbEx);
            }
            catch (Exception ex)
            {
                HandleError("suppression de la catégorie", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanEditOrDeleteCategory()
        {
            return SelectedCategorie != null && !IsBusy;
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null) message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}