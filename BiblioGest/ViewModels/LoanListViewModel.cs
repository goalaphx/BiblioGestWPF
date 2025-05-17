// Dans ViewModels/LoanListViewModel.cs
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
    public partial class LoanListViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private ObservableCollection<Emprunt> _loans = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ReturnBookCommand))]
        private Emprunt? _selectedLoan;

        // --- NEW: Search Functionality ---
        [ObservableProperty]
        private string? _searchText;

        private Timer? _searchDebounceTimer;
        private const int SearchDebounceTimeMs = 500;

        // TODO: Add properties for filtering (e.g., CurrentOnly, OverdueOnly)

        public LoanListViewModel(BiblioGestContext context, MainViewModel mainViewModel)
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
                    await LoadLoansAsync(null);
                });
            }, null, SearchDebounceTimeMs, Timeout.Infinite);
        }

        public override async Task LoadAsync()
        {
            await LoadLoansAsync(null); // Initial load
        }

        // Modified to accept parameter
        [RelayCommand]
        private async Task LoadLoansAsync(string? commandParameter)
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
            Loans.Clear();
            SelectedLoan = null;
            try
            {
                IQueryable<Emprunt> query = _context.Emprunts
                                                .Include(e => e.Livre)
                                                .Include(e => e.Adherent);

                // --- NEW: Apply Search Filter ---
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    string searchTextLower = SearchText.ToLower().Trim();
                    query = query.Where(e =>
                        (e.Livre != null && e.Livre.Titre != null && e.Livre.Titre.ToLower().Contains(searchTextLower)) ||
                        (e.Adherent != null && e.Adherent.Nom != null && e.Adherent.Nom.ToLower().Contains(searchTextLower)) ||
                        (e.Adherent != null && e.Adherent.Prenom != null && e.Adherent.Prenom.ToLower().Contains(searchTextLower)) ||
                        (e.Livre != null && e.Livre.ISBN != null && e.Livre.ISBN.ToLower().Contains(searchTextLower))
                        // Add more fields to search if needed (e.g., Adherent.Email)
                    );
                }

                var loansFromDb = await query
                                        .OrderBy(e => e.DateRetourEffective.HasValue)
                                        .ThenBy(e => e.DateRetourPrevue)
                                        .ToListAsync();
                foreach (var loan in loansFromDb)
                {
                    Loans.Add(loan);
                }
            }
            catch (Exception ex)
            {
                HandleError("chargement des emprunts", ex);
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

        // NewLoan, ReturnBook, CanReturnBook, HandleError methods remain the same
        [RelayCommand]
        private async Task NewLoan()
        {
            await _mainViewModel.NavigateToLoanNew();
        }

        [RelayCommand(CanExecute = nameof(CanReturnBook))]
        private async Task ReturnBook()
        {
            if (SelectedLoan == null || SelectedLoan.DateRetourEffective.HasValue)
            {
                MessageBox.Show("Veuillez sélectionner un emprunt en cours à retourner.", "Action Impossible", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var confirmResult = MessageBox.Show($"Confirmer le retour du livre '{SelectedLoan.Livre?.Titre}' par '{SelectedLoan.Adherent?.NomComplet}'?",
                                                "Confirmation de Retour", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmResult != MessageBoxResult.Yes) return;

            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var livre = await _context.Livres.FindAsync(SelectedLoan.LivreId);
                if (livre == null)
                {
                    HandleError("retour de livre", new InvalidOperationException($"Livre ID {SelectedLoan.LivreId} non trouvé."));
                    IsBusy = false; // Reset busy before returning
                    return;
                }
                SelectedLoan.DateRetourEffective = DateTime.UtcNow;
                livre.NombreExemplairesDisponibles++;
                _context.Emprunts.Update(SelectedLoan);
                _context.Livres.Update(livre);
                await _context.SaveChangesAsync();
                MessageBox.Show("Livre retourné avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadLoansAsync(null); // Refresh list
            }
            catch (DbUpdateConcurrencyException ex)
            {
                 HandleError("retour de livre (concurrence)", ex);
            }
            catch (Exception ex)
            {
                HandleError("retour de livre", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanReturnBook()
        {
            return SelectedLoan != null && !SelectedLoan.DateRetourEffective.HasValue && !IsBusy;
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null) message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}