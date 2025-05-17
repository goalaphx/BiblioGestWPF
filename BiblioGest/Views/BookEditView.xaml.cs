using System.Windows.Controls; // Required for UserControl

namespace BiblioGest.Views
{
    /// <summary>
    /// Logique d'interaction pour BookEditView.xaml
    /// </summary>
    public partial class BookEditView : UserControl
    {
        public BookEditView()
        {
            InitializeComponent();
            // IMPORTANT: DataContext (BookEditViewModel) set by DataTemplate in App.xaml
        }
    }
}