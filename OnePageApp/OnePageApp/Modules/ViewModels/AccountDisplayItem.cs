using System;
using OnePageApp.Framework;

namespace OnePageApp.Modules.ViewModels
{
    public class AccountDisplayItem
    {
        private readonly AppSettings appSettings;

        public AccountDisplayItem(
            AppSettings appSettings,
            Action<bool> onSelectionChanged)
        {
            this.appSettings = appSettings;
            this.OnSelectionChanged = onSelectionChanged;
        }

        public string Error { get; set; }

        public bool IsError { get; set; }

        
        public Action<bool> OnSelectionChanged;
    }

}
