// Dans ViewModels/LoanNewViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations; // For [Required]
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiblioGest.ViewModels
{
    public partial class LoanNewViewModel : BaseViewModel // Assuming BaseViewModel exists
    {
        private readonly BiblioGestContext _context;
        private readonly MainViewModel _mainViewModel;

        [ObservableProperty]
        private ObservableCollection<Adherent> _adherents = new();

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Veuillez sélectionner un adhérent.")]
        private Adherent? _selectedAdherent;

        [ObservableProperty]
        private ObservableCollection<Livre> _availableLivres = new();

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Veuillez sélectionner un livre.")]
        private Livre? _selectedLivre;

        [ObservableProperty]
        private DateTime _dateEmprunt;

        [ObservableProperty]
        private DateTime _dateRetourPrevue;

        [ObservableProperty]
        private string _windowTitle = "Nouvel Emprunt";

        // Configuration: Default loan duration in days
        private const int DefaultLoanDurationDays = 14;

        public LoanNewViewModel(BiblioGestContext context, MainViewModel mainViewModel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));

            // Initialize dates
            DateEmprunt = DateTime.UtcNow.Date; // Set to today (midnight UTC)
            CalculateDateRetourPrevue();
        }

        partial void OnDateEmpruntChanged(DateTime value)
        {
            // Recalculate due date when loan date changes
            CalculateDateRetourPrevue();
        }

        private void CalculateDateRetourPrevue()
        {
            DateRetourPrevue = DateEmprunt.AddDays(DefaultLoanDurationDays);
        }

        public override async Task LoadAsync() // Called when navigating to this view
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Adherents.Clear();
                var adherentsFromDb = await _context.Adherents
                                                  .OrderBy(a => a.Nom)
                                                  .ThenBy(a => a.Prenom)
                                                  .ToListAsync();
                foreach (var adherent in adherentsFromDb) Adherents.Add(adherent);

                AvailableLivres.Clear();
                var livresFromDb = await _context.Livres
                                                 .Where(l => l.NombreExemplairesDisponibles > 0) // Only available books
                                                 .OrderBy(l => l.Titre)
                                                 .ToListAsync();
                foreach (var livre in livresFromDb) AvailableLivres.Add(livre);

                // Reset selections
                SelectedAdherent = null;
                SelectedLivre = null;
                DateEmprunt = DateTime.UtcNow.Date; // Reset to today UTC
                ValidateAllProperties(); // Initial validation
            }
            catch (Exception ex)
            {
                HandleError("chargement des données pour nouvel emprunt", ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SaveLoanAsync()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                MessageBox.Show("Veuillez corriger les erreurs avant de sauvegarder.", "Erreurs de validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Additional check if somehow SelectedLivre became unavailable after loading
            if (SelectedLivre != null && SelectedLivre.NombreExemplairesDisponibles <= 0)
            {
                 MessageBox.Show($"Le livre '{SelectedLivre.Titre}' n'est plus disponible.", "Livre Indisponible", MessageBoxButton.OK, MessageBoxImage.Warning);
                 await LoadAsync(); // Refresh lists
                 return;
            }


            if (IsBusy) return;
            IsBusy = true;

            System.Diagnostics.Debug.WriteLine($"--- SaveLoanAsync ({DateTime.Now}) ---");
            System.Diagnostics.Debug.WriteLine($"Adherent ID: {SelectedAdherent!.Id}, Livre ID: {SelectedLivre!.Id}");
            System.Diagnostics.Debug.WriteLine($"DateEmprunt: {DateEmprunt:O} (Kind: {DateEmprunt.Kind}), DateRetourPrevue: {DateRetourPrevue:O} (Kind: {DateRetourPrevue.Kind})");

            try
            {
                // Fetch the selected Livre again to ensure we have the latest version for concurrency
                // and to update its tracked instance.
                var livreToUpdate = await _context.Livres.FindAsync(SelectedLivre!.Id);
                if (livreToUpdate == null)
                {
                    HandleError("sauvegarde de l'emprunt", new InvalidOperationException("Le livre sélectionné n'a pas été trouvé."));
                    IsBusy = false;
                    return;
                }
                if (livreToUpdate.NombreExemplairesDisponibles <= 0)
                {
                    MessageBox.Show($"Désolé, le livre '{livreToUpdate.Titre}' vient d'être emprunté et n'est plus disponible.", "Conflit de Disponibilité", MessageBoxButton.OK, MessageBoxImage.Warning);
                    await LoadAsync(); // Refresh the available books list
                    IsBusy = false;
                    return;
                }

                var newEmprunt = new Emprunt
                {
                    AdherentId = SelectedAdherent!.Id,
                    LivreId = livreToUpdate.Id,
                    // Ensure dates are UTC. DateEmprunt and DateRetourPrevue are already set as UTC .Date
                    DateEmprunt = DateTime.SpecifyKind(DateEmprunt, DateTimeKind.Utc),
                    DateRetourPrevue = DateTime.SpecifyKind(DateRetourPrevue, DateTimeKind.Utc),
                    DateRetourEffective = null // New loan, not returned yet
                };

                livreToUpdate.NombreExemplairesDisponibles--; // Decrement available copies

                _context.Emprunts.Add(newEmprunt);
                _context.Livres.Update(livreToUpdate); // Mark Livre as modified

                System.Diagnostics.Debug.WriteLine($"ChangeTracker before SaveChanges: Emprunts Added = {_context.ChangeTracker.Entries<Emprunt>().Count(e => e.State == EntityState.Added)}, Livres Modified = {_context.ChangeTracker.Entries<Livre>().Count(e => e.State == EntityState.Modified)}");

                int changes = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync completed. Changes: {changes}");


                if (changes > 0) // Should be 2 (1 for Emprunt, 1 for Livre update)
                {
                    MessageBox.Show("Emprunt enregistré avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _mainViewModel.RequestReturnToLoanList(); // Create this method in MainViewModel
                }
                else
                {
                     MessageBox.Show("L'emprunt n'a pas pu être enregistré. Aucune modification détectée.", "Erreur Sauvegarde", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (DbUpdateConcurrencyException ex) // Handle case where book availability changed
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateConcurrencyException: {ex.ToString()}");
                HandleError("sauvegarde de l'emprunt (conflit de données, ex: disponibilité du livre)", ex);
                await LoadAsync(); // Refresh data as it might be stale
            }
            catch (DbUpdateException dbEx)
            {
                System.Diagnostics.Debug.WriteLine($"DbUpdateException: {dbEx.ToString()}");
                HandleError("sauvegarde de l'emprunt (erreur base de données)", dbEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Exception: {ex.ToString()}");
                HandleError("sauvegarde de l'emprunt", ex);
            }
            finally
            {
                IsBusy = false;
                System.Diagnostics.Debug.WriteLine($"SaveLoanAsync finally block executed ({DateTime.Now}).");
            }
        }


        [RelayCommand]
        private async Task CancelAsync()
        {
            // TODO: Create RequestReturnToLoanList in MainViewModel
            await _mainViewModel.RequestReturnToLoanList();
        }

        private void HandleError(string operation, Exception ex)
        {
            string message = $"Erreur lors de {operation}: {ex.Message}";
            if (ex.InnerException != null) message += $"\nDétails: {ex.InnerException.Message}";
            MessageBox.Show(message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}