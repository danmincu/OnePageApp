using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Logs;
using MahApps.Metro.Controls.Dialogs;
using OnePageApp.Framework;
using OnePageApp.Model;
using OnePageApp.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace OnePageApp.Modules.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        AppSettings appSettings;
        IDialogCoordinator dialogCoordinator;
        IEventAggregator eventAggregator;
        private DelegateCommand saveSettingsCommand;

        public SettingsViewModel(
            IDialogCoordinator dialogCoordinator,
            AppSettings appSettings,
            IEventAggregator eventAggregator)
        {
            this.dialogCoordinator = dialogCoordinator;
            this.appSettings = appSettings;
            this.eventAggregator = eventAggregator;
            this.SaveSettingsCommand = new DelegateCommand(async () => await SaveSettingsCommandAsync());

            this.appSettings.Load();
            this.GetConfigSuccess = true;
            this.ConnectionString = this.appSettings.ConnectionString;
        }

        private string connectionString;
        public string ConnectionString
        {
            get => connectionString;
            set => SetProperty(ref connectionString, value);
        }

        private bool getConfigSuccess;
        public bool GetConfigSuccess
        {
            get => getConfigSuccess;
            set => SetProperty(ref getConfigSuccess, value);
        }

        public DelegateCommand SaveSettingsCommand
        {
            get => saveSettingsCommand;
            set => SetProperty(ref saveSettingsCommand, (DelegateCommand)value);
        }


        protected async Task SaveSettingsCommandAsync()
        {
            try
            {
                this.appSettings.ConnectionString = this.ConnectionString;
                this.appSettings.Save();
            }
            catch(Exception e)
            {
                Log.Add(e);
                await dialogCoordinator.ShowMessageAsync(this, "Failed", $"Exception: {e.Message}", MessageDialogStyle.Affirmative);
            }
        }
    }
}
