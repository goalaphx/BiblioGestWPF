// Dans ViewModels/MainViewModel.cs
using BiblioGest.Models; // For Adherent, Livre, Categorie
using BiblioGest.ViewModels; // For BaseViewModel and other ViewModels
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection; // Required for IServiceProvider
using System;
using System.Threading.Tasks;

namespace BiblioGest.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private BaseViewModel? _currentViewModel; // Starts as null, set by initial navigation

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            // Initial view will be set by MainWindow_Loaded calling one of the NavigateTo... commands.
        }

        // --- Navigation Commands (from Side Menu Buttons in MainWindow.xaml) ---

        [RelayCommand]
        public async Task NavigateToDashboard()
        {
            // Prevent re-navigation if already on Dashboard, unless forcing a refresh
            if (CurrentViewModel is DashboardViewModel && CurrentViewModel != null)
            {
                // Optionally, always reload dashboard data if navigating to it again from the menu
                // await CurrentViewModel.LoadAsync();
                return;
            }
            var vm = _serviceProvider.GetRequiredService<DashboardViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync();
        }

        [RelayCommand]
        public async Task NavigateToBooks()
        {
            await RequestReturnToBookList();
        }

        [RelayCommand]
        public async Task NavigateToMembers()
        {
            await RequestReturnToMemberList();
        }

        [RelayCommand]
        public async Task NavigateToLoans()
        {
            await RequestReturnToLoanList();
        }

        [RelayCommand]
        public async Task NavigateToCategories()
        {
            await RequestReturnToCategoryList();
        }

        // --- Methods to Navigate to Edit/New Screens (called by List ViewModels) ---

        public async Task NavigateToBookEdit(Livre? livre)
        {
            var vm = _serviceProvider.GetRequiredService<BookEditViewModel>();
            vm.SetLivre(livre); // Prepare with data (or null for new)
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync(); // BookEditViewModel loads categories
        }

        public async Task NavigateToMemberEdit(Adherent? adherent)
        {
            var vm = _serviceProvider.GetRequiredService<MemberEditViewModel>();
            vm.SetAdherent(adherent);
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync(); // MemberEditViewModel LoadAsync is empty but pattern is consistent
        }

        public async Task NavigateToLoanNew() // Typically called from LoanListViewModel
        {
            // Prevent re-navigation if already on the new loan screen (unlikely but safe)
            if (CurrentViewModel is LoanNewViewModel && CurrentViewModel != null) return;

            var vm = _serviceProvider.GetRequiredService<LoanNewViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync(); // LoanNewViewModel loads Adherents and available Livres
        }

        public async Task NavigateToCategoryEdit(Categorie? categorie)
        {
            var vm = _serviceProvider.GetRequiredService<CategoryEditViewModel>();
            vm.SetCategorie(categorie);
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync(); // CategoryEditViewModel LoadAsync is empty
        }


        // --- Methods to Return to List Screens (called by Edit/New ViewModels after save/cancel) ---

        public async Task RequestReturnToBookList()
        {
            if (CurrentViewModel is BookListViewModel && CurrentViewModel != null) return;
            var vm = _serviceProvider.GetRequiredService<BookListViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync();
        }

        public async Task RequestReturnToMemberList()
        {
            if (CurrentViewModel is MemberListViewModel && CurrentViewModel != null) return;
            var vm = _serviceProvider.GetRequiredService<MemberListViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync();
        }

        public async Task RequestReturnToLoanList()
        {
            if (CurrentViewModel is LoanListViewModel && CurrentViewModel != null) return;
            var vm = _serviceProvider.GetRequiredService<LoanListViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync();
        }

        public async Task RequestReturnToCategoryList()
        {
            if (CurrentViewModel is CategoryListViewModel && CurrentViewModel != null) return;
            var vm = _serviceProvider.GetRequiredService<CategoryListViewModel>();
            CurrentViewModel = vm;
            await CurrentViewModel.LoadAsync();
        }
    }
}