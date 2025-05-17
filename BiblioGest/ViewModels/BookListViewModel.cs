// Dans ViewModels/BookListViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading; // Required for System.Threading.Timer and Timeout
using System.Threading.Tasks;
using System.Windows; // Required for Application.Current.Dispatcher

namespace BiblioGest.ViewModels
{
    public partial class BookListViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private ObservableCollection<Livre> _livres = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditBookCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteBookCommand))]
        private Livre? _selectedLivre;

        [ObservableProperty]
        private string? _searchText;

        private Timer? _searchDebounceTimer;
        private const int SearchDebounceTimeMs = 500; // 0.5 second delay

        public BookListViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
        }

        // Called when SearchText changes (CommunityToolkit.Mvvm auto-generated partial method)
        async partial void OnSearchTextChanged(string? value)
        {
            _searchDebounceTimer?.Dispose();

            _searchDebounceTimer = new Timer(async _ =>
            {
                // Ensure execution on the UI thread for LoadBooksAsync
                await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    await LoadBooksAsync(null); // Pass null or a different parameter to distinguish from button click
                });
            }, null, SearchDebounceTimeMs, Timeout.Infinite);
        }

        public override async Task LoadAsync() => await LoadBooksAsync(null); // Initial load without specific parameter

        [RelayCommand]
        private async Task LoadBooksAsync(string? commandParameter)
        {
            // If the command parameter indicates clearing search, set SearchText to empty.
            // This will trigger OnSearchTextChanged, which will then call this method again after debounce.
            // To avoid double loading, we can check if SearchText was just cleared.
            bool justClearedSearch = false;
            if ("clear_search".Equals(commandParameter))
            {
                if (!string.IsNullOrEmpty(SearchText)) // Only clear and trigger if there's text
                {
                    SearchText = string.Empty;
                    justClearedSearch = true; // Mark that we just cleared it
                }
                // If SearchText was already empty, the "Actualiser" should still reload all books.
            }

            // If search was just cleared, OnSearchTextChanged will handle the reload, so we can exit here.
            // Or, if you want "Actualiser" to be immediate even when clearing, remove this check.
            if (justClearedSearch && _searchDebounceTimer != null) // Check if timer was set by OnSearchTextChanged
            {
                 // The OnSearchTextChanged will trigger the load.
                 // We might still want to set IsBusy if the user expects immediate feedback.
                 // However, to avoid complexity, let the debounce handle the busy state.
                return;
            }


            if (IsBusy) return;
            IsBusy = true;
            Livres.Clear();
            SelectedLivre = null;
            try
            {
                IQueryable<Livre> query = _context.Livres.Include(l => l.Categorie);

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    string searchTextLower = SearchText.ToLower().Trim();
                    query = query.Where(l =>
                        (l.Titre != null && l.Titre.ToLower().Contains(searchTextLower)) ||
                        (l.Auteur != null && l.Auteur.ToLower().Contains(searchTextLower)) ||
                        (l.ISBN != null && l.ISBN.ToLower().Contains(searchTextLower)) ||
                        (l.Editeur != null && l.Editeur.ToLower().Contains(searchTextLower)) ||
                        (l.Categorie != null && l.Categorie.Nom.ToLower().Contains(searchTextLower))
                    );
                }

                var books = await query.OrderBy(l => l.Titre).ToListAsync();
                foreach (var book in books) Livres.Add(book);
            }
            catch (Exception ex) { HandleError("chargement des livres", ex); }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = string.Empty; // This will trigger OnSearchTextChanged, then LoadBooksAsync
            }
            // If SearchText is already empty, no action needed for clear button.
        }


        [RelayCommand]
        private async Task AddBook() => await _mainViewModel.NavigateToBookEdit(null);

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteBook))]
        private async Task EditBook()
        {
            if (SelectedLivre != null)
                await _mainViewModel.NavigateToBookEdit(SelectedLivre);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteBook))]
        private async Task DeleteBookAsync()
        {
            if (SelectedLivre == null) return;
            var result = MessageBox.Show($"Supprimer '{SelectedLivre.Titre}' ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes) return;

            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var toDelete = await _context.Livres.FindAsync(SelectedLivre.Id);
                if (toDelete != null)
                {
                    _context.Livres.Remove(toDelete);
                    await _context.SaveChangesAsync();
                    Livres.Remove(SelectedLivre);
                    SelectedLivre = null;
                }
                else
                {
                    MessageBox.Show("Le livre sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (SelectedLivre != null) Livres.Remove(SelectedLivre); // Ensure UI consistency
                    SelectedLivre = null;
                }
            }
            catch (DbUpdateException dbEx) { HandleError("suppression (vérifiez les emprunts actifs)", dbEx); }
            catch (Exception ex) { HandleError("suppression du livre", ex); }
            finally { IsBusy = false; }
        }

        private bool CanEditOrDeleteBook() => SelectedLivre != null && !IsBusy;

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null)
                message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}