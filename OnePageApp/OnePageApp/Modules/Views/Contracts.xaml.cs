using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using OnePageApp.Modules.ViewModels;

namespace OnePageApp.Modules.Views
{
    /// <summary>
    /// Interaction logic for Contracts.xaml
    /// </summary>
    public partial class Contracts : UserControl
    {

        private IChildWindowLauncher childWindowLauncher;
        private IDialogCoordinator dialogCoordinator;

        public Contracts(IChildWindowLauncher childWindowLauncher, IDialogCoordinator dialogCoordinator)
        {
            this.childWindowLauncher = childWindowLauncher;
            this.dialogCoordinator = dialogCoordinator;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await childWindowLauncher.ShowChildWindowAsync(new SimpleChildModel(null));
            if (!result)
            {
                await dialogCoordinator.ShowMessageAsync(this.DataContext, "Failed", "You must select your default Item!", MessageDialogStyle.Affirmative);
            }
        }
    }
}
