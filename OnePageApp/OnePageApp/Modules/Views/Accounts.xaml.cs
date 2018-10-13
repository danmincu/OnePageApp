using System.Windows.Controls;
using OnePageApp.Modules.ViewModels;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : UserControl
    {
        public Accounts(AccountsViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
