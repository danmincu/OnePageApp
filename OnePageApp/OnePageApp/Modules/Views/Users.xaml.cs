using System.Windows.Controls;
using OnePageApp.Modules.ViewModels;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public Users(UsersViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
