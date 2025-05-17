// Dans ViewModels/DashboardViewModel.cs
using BiblioGest.Data;
using BiblioGest.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; // For List
using System.Collections.ObjectModel; // For ObservableCollection
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media; // For Color/Brush (optional for styling)

namespace BiblioGest.ViewModels
{
    // Helper class for category statistics
    public partial class CategoryBookCountViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _categoryName;

        [ObservableProperty]
        private int _bookCount;

        [ObservableProperty]
        private double _percentage; // For progress bar visualization

        [ObservableProperty]
        private Brush? _barColor; // For distinct progress bar colors
    }

    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly BiblioGestContext _context;

        [ObservableProperty]
        private int _totalLivres;

        [ObservableProperty]
        private int _totalAdherentsActifs;

        [ObservableProperty]
        private int _empruntsEnCours;

        [ObservableProperty]
        private int _empruntsEnRetard;

        [ObservableProperty]
        private string _welcomeMessage = "Bienvenue sur BiblioGest !";

        // --- NEW Properties for more detailed stats ---
        [ObservableProperty]
        private ObservableCollection<CategoryBookCountViewModel> _categoryBookCounts = new();

        [ObservableProperty]
        private ObservableCollection<Emprunt> _recentLoans = new(); // Top 5 recent loans

        [ObservableProperty]
        private int _livresDisponibles;


        public DashboardViewModel(BiblioGestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task LoadAsync()
        {
            await LoadDashboardDataAsync();
        }

        [RelayCommand]
        private async Task LoadDashboardDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            System.Diagnostics.Debug.WriteLine($"--- LoadDashboardDataAsync ({DateTime.Now}) ---");
            try
            {
                // --- Basic Stats ---
                TotalLivres = await _context.Livres.CountAsync();
                LivresDisponibles = await _context.Livres.SumAsync(l => l.NombreExemplairesDisponibles); // Total available copies
                TotalAdherentsActifs = await _context.Adherents.CountAsync(a => a.Statut == StatutAdherent.Actif);
                var todayUtc = DateTime.UtcNow.Date;
                EmpruntsEnCours = await _context.Emprunts.CountAsync(e => e.DateRetourEffective == null);
                EmpruntsEnRetard = await _context.Emprunts.CountAsync(e => e.DateRetourEffective == null && e.DateRetourPrevue < todayUtc);
                WelcomeMessage = $"Tableau de Bord - {DateTime.Now:dddd, dd MMMM yyyy}";

                // --- Livres par Catégorie ---
                CategoryBookCounts.Clear();
                var categoryStats = await _context.Categories
                                          .Select(c => new
                                          {
                                              c.Nom,
                                              BookCount = c.Livres.Count() // Counts books related to this category
                                          })
                                          .Where(cs => cs.BookCount > 0) // Only show categories with books
                                          .OrderByDescending(cs => cs.BookCount)
                                          .ToListAsync();

                int totalBooksForPercentage = categoryStats.Sum(cs => cs.BookCount); // Could also use TotalLivres if it represents unique titles
                if (totalBooksForPercentage == 0) totalBooksForPercentage = 1; // Avoid division by zero

                var colors = new List<Brush> { Brushes.SkyBlue, Brushes.LightGreen, Brushes.Salmon, Brushes.Gold, Brushes.LightSteelBlue, Brushes.Plum };
                int colorIndex = 0;

                foreach (var stat in categoryStats)
                {
                    CategoryBookCounts.Add(new CategoryBookCountViewModel
                    {
                        CategoryName = stat.Nom,
                        BookCount = stat.BookCount,
                        Percentage = totalBooksForPercentage > 0 ? (double)stat.BookCount / totalBooksForPercentage * 100 : 0,
                        BarColor = colors[colorIndex++ % colors.Count] // Cycle through predefined colors
                    });
                }

                // --- Emprunts Récents (e.g., last 5 loans made) ---
                RecentLoans.Clear();
                var recent = await _context.Emprunts
                                    .Include(e => e.Livre)
                                    .Include(e => e.Adherent)
                                    .OrderByDescending(e => e.DateEmprunt)
                                    .Take(5) // Get top 5 most recent
                                    .ToListAsync();
                foreach (var loan in recent) RecentLoans.Add(loan);

            }
            catch (Exception ex)
            {
                TotalLivres = 0;
                TotalAdherentsActifs = 0;
                EmpruntsEnCours = 0;
                EmpruntsEnRetard = 0;
                LivresDisponibles = 0;
                CategoryBookCounts.Clear();
                RecentLoans.Clear();
                WelcomeMessage = "Erreur de chargement des données.";
                System.Diagnostics.Debug.WriteLine($"ERROR loading dashboard data: {ex.ToString()}");
            }
            finally
            {
                IsBusy = false;
                System.Diagnostics.Debug.WriteLine($"--- LoadDashboardDataAsync finished ({DateTime.Now}) ---");
            }
        }
    }
}