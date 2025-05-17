using BiblioGest.ViewModels;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BiblioGest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (App.ServiceProvider != null)
            {
                this.DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
            }
            else
            {
                MessageBox.Show("Erreur critique : Le fournisseur de services n'a pas été initialisé.", "Erreur Démarrage", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Application.Current != null) Application.Current.Shutdown();
            }

            Loaded += MainWindow_Loaded;
        }

        // Modified Loaded event
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Unsubscribe immediately to prevent running multiple times
            Loaded -= MainWindow_Loaded;

            // Ensure DataContext is MainViewModel
            if (DataContext is MainViewModel mainViewModel)
            {
                // --- Trigger Initial Navigation ---
                // Check if a view is already set (less likely now, but safe check)
                if (mainViewModel.CurrentViewModel == null)
                {
                    try
                    {
                        // Explicitly navigate to the default starting view
                        // This will resolve MemberListViewModel and call its LoadAsync
                        await mainViewModel.NavigateToDashboard(); // Or NavigateToBooks() if you prefer
                    }
                    catch (Exception ex)
                    {
                        // Handle errors during initial navigation/load
                        MessageBox.Show($"Erreur lors du chargement de la vue initiale : {ex.Message}",
                                        "Erreur de Chargement", MessageBoxButton.OK, MessageBoxImage.Error);
                        System.Diagnostics.Debug.WriteLine($"ERROR Initial View Load: {ex}");
                    }
                }
                // --- Optional: If CurrentViewModel WAS somehow set earlier ---
                // else if (mainViewModel.CurrentViewModel is BaseViewModel initialViewModel)
                // {
                //     // Load data if not done by navigation
                //     try
                //     {
                //         initialViewModel.IsBusy = true;
                //         await initialViewModel.LoadAsync();
                //     } catch //... finally...
                // }
            }
        }
    }
}