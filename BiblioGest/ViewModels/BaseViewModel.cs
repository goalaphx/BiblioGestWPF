// Dans ViewModels/BaseViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace BiblioGest.ViewModels
{
    // Inherits from ObservableValidator for validation support
    public abstract partial class BaseViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;

        // Abstract method to ensure derived ViewModels implement loading logic
        public abstract Task LoadAsync();

        // Optional: Common error handling or other shared logic can go here
    }
}