using OnePageApp.Modules.ViewModels;
using System.Windows.Controls;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            this.DataContext = settingsViewModel;
        }
    }
}
