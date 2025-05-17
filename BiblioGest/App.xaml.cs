using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// Removed: using Microsoft.Extensions.Hosting;
using System;
using System.IO; // Required for Path and Directory
using System.Windows;
using BiblioGest.ViewModels;
using BiblioGest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;


namespace BiblioGest
{
    public partial class App : Application
    {
        // Store the Service Provider directly
        public static IServiceProvider ServiceProvider { get; private set; } = null!; // Add null-forgiving operator
        private IConfiguration Configuration { get; set; } = null!; // Store configuration

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); // Call base WPF startup

            try
            {
                // 1. Build Configuration (Manual alternative to HostBuilder)
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Or AppContext.BaseDirectory
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                Configuration = builder.Build();

                // 2. Configure Services
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // 3. Build Service Provider
                ServiceProvider = serviceCollection.BuildServiceProvider();

                // --- Startup Window ---
                // Resolve the MainWindow instance
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show(); // Display the main window
            }
            catch (Exception ex)
            {
                // Catch critical errors during startup
                string errorMsg = $"Erreur critique lors du démarrage de l'application: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMsg += $"\nInner Exception: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMsg + "\n\nVérifiez appsettings.json et les logs.",
                                "Erreur de Démarrage", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Diagnostics.Debug.WriteLine($"FATAL STARTUP ERROR: {ex}");

                // Attempt to shutdown gracefully
                if (Application.Current != null)
                {
                    Application.Current.Shutdown(-1); // Use a non-zero exit code for error
                }
                Environment.Exit(-1); // Force exit if shutdown fails
            }
        }

        // Separate method to configure services (cleaner)
        private void ConfigureServices(IServiceCollection services)
        {
            // --- Database Context ---
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("La chaîne de connexion 'DefaultConnection' est manquante ou vide dans appsettings.json.",
                                "Erreur de Configuration Critique", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found or is empty.");
            }
            services.AddDbContext<BiblioGestContext>(options =>
                options.UseNpgsql(connectionString));

            // --- ViewModels & Core Services ---
            // Register MainViewModel as Singleton (lives for the app lifetime)
            services.AddSingleton<MainViewModel>();
            // In App.xaml.cs, inside ConfigureServices method
            services.AddTransient<DashboardViewModel>(); // Add this line

            // Register other ViewModels as Transient (new instance each time requested)
            services.AddTransient<BookListViewModel>();
            services.AddTransient<BookEditViewModel>();
            services.AddTransient<MemberListViewModel>();
            services.AddTransient<MemberEditViewModel>();
            services.AddTransient<LoanListViewModel>();
            services.AddTransient<LoanNewViewModel>(); 
            // In App.xaml.cs, inside ConfigureServices method
            services.AddTransient<CategoryListViewModel>();
            services.AddTransient<CategoryEditViewModel>(); // Add this line
            // Register future ViewModels here as Transient

            // --- Views (Windows/UserControls) ---
            // Register MainWindow as Singleton.
            // Note: DataContext is now set slightly differently.
            services.AddSingleton<MainWindow>();

            // IMPORTANT: Since we resolve MainWindow *before* its DataContext might be set,
            // we need to ensure the DataContext is set *after* resolution or in the constructor.
            // The easiest way is often in the MainWindow constructor using the static ServiceProvider.
            // Alternatively, set it after resolving in OnStartup (shown below, slightly less clean).

            // OPTION 1 (Recommended): Set DataContext in MainWindow constructor (see modification below)
            // OPTION 2 (Alternative): Set DataContext after resolving MainWindow in OnStartup
            // var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            // mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>(); // Set it manually
            // mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Clean up resources if needed (e.g., dispose ServiceProvider if it holds IDisposable)
            // (Often not strictly necessary for simple cases as process exit handles it)
            base.OnExit(e);
        }
    }
}