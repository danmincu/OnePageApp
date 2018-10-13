using System.Windows.Controls;
using MahApps.Metro.Controls;
using OnePageApp.Modules.ViewModels;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu(MenuViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is ViewModels.MenuItem menuItem && menuItem.IsNavigation)
            {
                //Navigation.Navigate(menuItem.NavigationDestination());
                menuItem.NavigationDestination();
            }
        }
    }
}
