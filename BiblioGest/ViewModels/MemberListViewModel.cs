// Dans ViewModels/MemberListViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;     // Required for System.Threading.Timer and Timeout
using System.Threading.Tasks;
using System.Windows;     // Required for Application.Current.Dispatcher

namespace BiblioGest.ViewModels
{
    public partial class MemberListViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private ObservableCollection<Adherent> _adherents = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditMemberCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteMemberCommand))]
        private Adherent? _selectedAdherent;

        // --- NEW: Search Text Property ---
        [ObservableProperty]
        private string? _searchText;

        // --- NEW: Debounce timer for search ---
        private Timer? _searchDebounceTimer;
        private const int SearchDebounceTimeMs = 500; // 0.5 second delay

        public MemberListViewModel(BiblioGestContext context, MainViewModel mainViewModel)
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
                    await LoadMembersAsync(null); // Pass null or different parameter
                });
            }, null, SearchDebounceTimeMs, Timeout.Infinite);
        }

        public override async Task LoadAsync()
        {
            await LoadMembersAsync(null); // Initial load
        }

        // Modified to accept a parameter for the "Actualiser" button
        [RelayCommand]
        private async Task LoadMembersAsync(string? commandParameter)
        {
            bool justClearedSearch = false;
            if ("clear_search".Equals(commandParameter))
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    SearchText = string.Empty;
                    justClearedSearch = true;
                }
            }
            if (justClearedSearch && _searchDebounceTimer != null) return;


            if (IsBusy) return;
            IsBusy = true;
            Adherents.Clear();
            SelectedAdherent = null;
            try
            {
                IQueryable<Adherent> query = _context.Adherents.AsQueryable();

                // --- NEW: Apply Search Filter ---
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    string searchTextLower = SearchText.ToLower().Trim();
                    query = query.Where(a =>
                        (a.Nom != null && a.Nom.ToLower().Contains(searchTextLower)) ||
                        (a.Prenom != null && a.Prenom.ToLower().Contains(searchTextLower)) ||
                        (a.Email != null && a.Email.ToLower().Contains(searchTextLower)) ||
                        (a.Telephone != null && a.Telephone.ToLower().Contains(searchTextLower)) ||
                        (a.Adresse != null && a.Adresse.ToLower().Contains(searchTextLower))
                    );
                }

                var membersFromDb = await query
                                          .OrderBy(a => a.Nom)
                                          .ThenBy(a => a.Prenom)
                                          .ToListAsync();
                foreach (var member in membersFromDb)
                {
                    Adherents.Add(member);
                }
            }
            catch (Exception ex)
            {
                HandleError("chargement des adhérents", ex);
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

        [RelayCommand]
        private async Task AddMember()
        {
            await _mainViewModel.NavigateToMemberEdit(null);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteMember))]
        private async Task EditMember()
        {
            if (SelectedAdherent == null) return;
            await _mainViewModel.NavigateToMemberEdit(SelectedAdherent);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDeleteMember))]
        private async Task DeleteMemberAsync()
        {
            if (SelectedAdherent == null) return;
            var result = MessageBox.Show($"Supprimer l'adhérent '{SelectedAdherent.NomComplet}' ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (IsBusy) return;
                IsBusy = true;
                try
                {
                    var memberToDelete = await _context.Adherents.FindAsync(SelectedAdherent.Id);
                    if (memberToDelete != null)
                    {
                        _context.Adherents.Remove(memberToDelete);
                        await _context.SaveChangesAsync();
                        Adherents.Remove(SelectedAdherent);
                        SelectedAdherent = null;
                    }
                    else
                    {
                        MessageBox.Show("L'adhérent sélectionné n'a pas été trouvé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                        if(SelectedAdherent != null) Adherents.Remove(SelectedAdherent);
                        SelectedAdherent = null;
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    HandleError("suppression (vérifiez les emprunts actifs)", dbEx);
                }
                catch (Exception ex)
                {
                    HandleError("suppression de l'adhérent", ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private bool CanEditOrDeleteMember()
        {
            return SelectedAdherent != null && !IsBusy;
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null) message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}