using MahApps.Metro.Controls.Dialogs;
using OnePageApp.Framework;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using MahApps.Metro.IconPacks;


namespace OnePageApp.Modules.ViewModels
{
    public class MenuViewModel: BindableBase
    {
        private IEventAggregator eventAggregator;
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();

        public MenuViewModel(IDialogCoordinator dialogCoordinator, AppSettings appSettings, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CalendarRegular }, Text = "Accounts", NavigationDestination = () => this.NavigateTo(AppMenu, 0) });
            Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UserCogSolid }, Text = "Permissions", NavigationDestination = () => this.NavigateTo(AppMenu, 1) });
            Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.FileContractSolid }, Text = "Contracts", NavigationDestination = () => this.NavigateTo(AppMenu, 2) });
            OptionsMenu.Add(new MenuItem() { Icon = Application.Current.FindResource("SettingsIcon"), Text = "Settings", NavigationDestination = () => this.NavigateTo(AppOptionsMenu, 0) });


            this.eventAggregator.GetEvent<NavigationInfoEvent>().Subscribe(OnNavigationInfoEvent, ThreadOption.UIThread, true);

            this.SelectedOptionsIndex = -1;
            this.SelectedIndex = -1;
            this.IsEnabled = true;
        }

        private void OnNavigationInfoEvent(string path)
        {
            switch (path)
            {
                case "IsLoading=False":
                    this.IsEnabled = true;
                    break;
                case "IsLoading=True":
                    this.IsEnabled = false;
                    break;

                case nameof(Views.Settings):
                    SelectedIndex = -1;
                    SelectedOptionsIndex = 0;
                    break;

                case nameof(Views.Accounts):
                    SelectedIndex = 0;
                    SelectedOptionsIndex = -1;
                    break;

                
                case nameof(Views.Permissions):
                    SelectedIndex = 1;
                    SelectedOptionsIndex = -1;
                    break;

                case nameof(Views.Contracts):
                    SelectedIndex = 2;
                    SelectedOptionsIndex = -1;
                    break;

                default:
                    SelectedIndex = -1;
                    SelectedOptionsIndex = -1;
                    break;
            }
        }

        private string[] navigationArray = new[] { nameof(Views.Accounts), nameof(Views.Permissions), nameof(Views.Contracts) };
        private string[] navigationOptionsArray = new[] { nameof(Views.Settings) };

        private object NavigateTo(object sender, int index)
        {
            var label = sender == AppMenu ? navigationArray[index] : navigationOptionsArray[index];
            this.eventAggregator.GetEvent<NavigationEvent>().Publish(label);
            return label;
        }

        private int selectedOptionsIndex;

        public int SelectedOptionsIndex
        {
            get => selectedOptionsIndex;
            set => SetProperty(ref selectedOptionsIndex, value);
        }

        private int selectedIndex;
        private bool isEnabled;

        public int SelectedIndex
        {
            get => selectedIndex;
            set => SetProperty(ref selectedIndex, value);
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        public ObservableCollection<MenuItem> Menu => AppMenu;

        public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;


    }
}
