using System.Windows;
using MahApps.Metro.SimpleChildWindow;

namespace OnePageApp.Modules.Views.ChildWindows
{
    /// <summary>
    /// Interaction logic for SimpleChildWindow.xaml
    /// </summary>
    public partial class AddUserWindow : ChildWindow
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            this.Close(true);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
