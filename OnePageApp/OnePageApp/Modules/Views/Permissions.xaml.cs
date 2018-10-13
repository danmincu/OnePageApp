using OnePageApp.Modules.ViewModels;
using System.Windows.Controls;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Permissions.xaml
    /// </summary>
    public partial class Permissions : UserControl
    {
        public Permissions(PermissionViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
