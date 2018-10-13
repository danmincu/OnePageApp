using System;
using Prism.Commands;
using Prism.Mvvm;

namespace OnePageApp.Modules.ViewModels
{
    public class MenuItem : BindableBase
    {
        private object icon;
        private string text;
        private bool isEnabled = true;
        private DelegateCommand command;
        private Func<object> navigationDestination;


        public object Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        public Func<object> NavigationDestination
        {
            get => navigationDestination;
            set => SetProperty(ref navigationDestination, value);
        }

        public bool IsNavigation => navigationDestination != null;
    }
}
