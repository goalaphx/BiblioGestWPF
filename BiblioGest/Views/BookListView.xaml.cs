using System.Windows.Controls; // Required for UserControl

namespace BiblioGest.Views
{
    /// <summary>
    /// Logique d'interaction pour BookListView.xaml
    /// </summary>
    public partial class BookListView : UserControl
    {
        public BookListView()
        {
            InitializeComponent();
            // IMPORTANT: The DataContext (BookListViewModel) will be set automatically
            // by the DataTemplate in App.xaml when this view is displayed.
            // DO NOT set the DataContext here manually if using DataTemplates.
        }
    }
}