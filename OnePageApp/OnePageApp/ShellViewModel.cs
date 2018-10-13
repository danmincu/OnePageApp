using System.Windows;
using System.Windows.Markup;
using OnePageApp.Framework;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace OnePageApp
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private bool isBackEnabled = false;
        private Visibility showNavigation;
        private bool isEnabled;

        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand(Navigate, CanExecuteMethod);
            NavigateCommand.ObservesCanExecute(() => this.IsBackEnabled);

            this.eventAggregator.GetEvent<ControlBackFlowEvent>().Subscribe(ControlActionBackFlowEvent, ThreadOption.UIThread, true);
            this.eventAggregator.GetEvent<NavigationInfoEvent>().Subscribe(OnNavigationInfoEvent, ThreadOption.UIThread, true);
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
                default:
                    break;
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        private void ControlActionBackFlowEvent(bool isEnabled)
        {
            IsBackEnabled = isEnabled;
        }

        public bool IsBackEnabled
        {
            get { return isBackEnabled; }
            set
            {
                SetProperty(ref isBackEnabled, value);
                OnPropertyChanged("NavigateVisibility");
            }
        }

        [DependsOn("IsBackEnabled")]
        public Visibility NavigateVisibility => isBackEnabled ? Visibility.Visible : Visibility.Collapsed;


        private bool CanExecuteMethod()
        {
            return this.IsBackEnabled;
        }

        private void Navigate()
        {
            if (this.IsEnabled)
            {
                this.eventAggregator.GetEvent<NavigationEvent>().Publish("NavigateBack");
            }
        }

        public DelegateCommand NavigateCommand { get; private set; }

    }
}
