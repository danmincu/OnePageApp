using System;
using System.Threading.Tasks;
using System.Windows;
using Logs;
using MahApps.Metro.Controls.Dialogs;
using OnePageApp.Framework;
using OnePageApp.Model;
using Prism.Commands;
using Prism.Events;

namespace OnePageApp.Modules.ViewModels
{
    public class AccountsViewModel : BaseSyncViewModel<AccountItem, AccountDisplayItem>
    {
        private bool allCurrentSelected = true;

    
        public AccountsViewModel(
            IDialogCoordinator dialogCoordinator,
            AppSettings appSettings,
            IEventAggregator eventAggregator) :
            base(dialogCoordinator,
                appSettings,
                eventAggregator)
        {

            SetupItemsCommand = new DelegateCommand(async () => await SetupItemCommandAsync()).ObservesCanExecute(() => this.HasItemsSelected);
        }

        public bool AllCurrentSelected
        {
            get => allCurrentSelected;
            set
            {
                SetProperty(ref allCurrentSelected, value);
            }
        }

        protected override bool CanExecuteFetch()
        {
            return true;
        }

        protected override async Task ExecuteFetchAsync()
        {
            try
            {
   
            }
            catch (Exception ex)
            {
                Log.Add(ex);
                await dialogCoordinator.ShowMessageAsync(this, "Error", "There was an error fetching data.");
            }
            finally
            {
                this.SetIsLoading(false);
            }
        }

        protected override async Task ExecuteValidateAsync()
        {
            // we don't this this step for orders
            throw new NotImplementedException();
        }

        protected override async Task SetupItemsAsync()
        {
            this.SetIsLoading(true);

            try
            {
       
            }
            finally
            {
                this.SetIsLoading(false);
            }
        }

        protected override void OnCurrentSelectedItemChanged(bool isChecked)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.allCurrentSelected = false;
                this.RaisePropertyChanged(nameof(AllCurrentSelected));
            });
        }

        protected override async void ExecuteSync()
        {
            var reconcilableException = false;

            this.SetIsLoading(true);

            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                this.SetIsLoading(false);
                if (!reconcilableException)
                {
                    await ExecuteFetchAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
