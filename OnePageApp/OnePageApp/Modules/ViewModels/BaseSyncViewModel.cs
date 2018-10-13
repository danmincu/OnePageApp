using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using OnePageApp.Framework;
using OnePageApp.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace OnePageApp.Modules.ViewModels
{
    public abstract class BaseSyncViewModel<T, TT> : BindableBase
       where T : class, ISelectable
       where TT : class
    {
        public AppSettings AppSettings { get; }
        protected readonly IDialogCoordinator dialogCoordinator;

        public ObservableCollection<T> ItemCollection { get; set; }


        public ObservableCollection<TT> ChoosenDisplayItemsList;


        private DateTime startDate;
        private DelegateCommand fetchRecordsCommand;
        private DelegateCommand syncRecordsCommand;
        private bool isLoading;
        private bool allSelected;

        protected IEventAggregator eventAggregator;
        private DelegateCommand validateRecordsCommand;
        private DelegateCommand setupItemsCommand;
        private DelegateCommand nextPartCommand;
        private DelegateCommand prevPartCommand;

        protected ListCollectionView partsCollectionView;

        public ListCollectionView PartsCollectionView
        {
            get => partsCollectionView;
            set => SetProperty(ref partsCollectionView, value);
        }

        public bool AllSelected
        {
            get => allSelected;
            set
            {
                SetProperty(ref allSelected, value);
                foreach (var item in ItemCollection)
                {
                    item.IsSelected = value;
                }
            }
        }

        protected void OnSelectionChanged(bool isChecked)
        {
            this.HasItemsSelected = this.ItemCollection.Any(itm => itm.IsSelected);
            this.allSelected = this.ItemCollection.All(itm => itm.IsSelected);
            OnPropertyChanged("AllSelected");
        }

        protected abstract void OnCurrentSelectedItemChanged(bool isChecked);

        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public void SetIsLoading(bool value)
        {
            this.eventAggregator.GetEvent<NavigationInfoEvent>().Publish($"IsLoading={value}");
            this.IsLoading = value;
        }

        public DelegateCommand FetchRecordsCommand
        {
            get => fetchRecordsCommand;
            set => SetProperty(ref fetchRecordsCommand, (DelegateCommand)value);
        }

        public DelegateCommand SyncRecordsCommand
        {
            get => syncRecordsCommand;
            set => SetProperty(ref syncRecordsCommand, (DelegateCommand)value);
        }

        public DelegateCommand NextPartCommand
        {
            get => nextPartCommand;
            set => SetProperty(ref nextPartCommand, (DelegateCommand)value);
        }

        public DelegateCommand PrevPartCommand
        {
            get => prevPartCommand;
            set => SetProperty(ref prevPartCommand, (DelegateCommand)value);
        }

        public DelegateCommand ValidateRecordsCommand
        {
            get => validateRecordsCommand;
            set => SetProperty(ref validateRecordsCommand, (DelegateCommand)value);
        }

        public DelegateCommand SetupItemsCommand
        {
            get => setupItemsCommand;
            set => SetProperty(ref setupItemsCommand, (DelegateCommand)value);
        }

        //lock object for synchronization;
        protected static object _syncLock = new object();

        public BaseSyncViewModel(IDialogCoordinator dialogCoordinator,
            AppSettings appSettings,
            IEventAggregator eventAggregator)
        {
            AppSettings = appSettings;

            this.eventAggregator = eventAggregator;
            this.dialogCoordinator = dialogCoordinator;
            StartDate = DateTime.UtcNow.AddDays(-7);
            ItemCollection = new ObservableCollection<T>();
            BindingOperations.EnableCollectionSynchronization(ItemCollection, _syncLock);


            this.ChoosenDisplayItemsList = new ObservableCollection<TT>();
            BindingOperations.EnableCollectionSynchronization(this.ChoosenDisplayItemsList, _syncLock);
            this.PartsCollectionView = new ListCollectionView(this.ChoosenDisplayItemsList);


            FetchRecordsCommand = new DelegateCommand(async () => await ExecuteFetchAsync(), CanExecuteFetch);
            SyncRecordsCommand = new DelegateCommand(ExecuteSync).ObservesCanExecute(() => HasCurrentItemsSelectedAndValid);
            ValidateRecordsCommand = new DelegateCommand(async () => await ExecuteValidateAsync()).ObservesCanExecute(() => this.HasItemsSelected); ;
            //SetupItemsCommand = new DelegateCommand(async () => await SetupItemCommandAsync()).ObservesCanExecute(() => this.SelectedItemsAreValid);
            NextPartCommand = new DelegateCommand(NextPart).ObservesCanExecute(() => this.HasNext);
            PrevPartCommand = new DelegateCommand(PrevPart).ObservesCanExecute(() => this.HasPrev);
            this.SetIsLoading(false);
        }

        public bool HasCurrentItemsSelectedAndValid
        {
            get => hasCurrentItemsSelectedAndValid;
            set => SetProperty(ref hasCurrentItemsSelectedAndValid, value);
        }

        public bool HasItemsSelected
        {
            get => hasItemsSelected;
            set => SetProperty(ref hasItemsSelected, value);
        }

        public bool SelectedItemsAreValid
        {
            get => selectedItemsAreValid;
            set => SetProperty(ref selectedItemsAreValid, value);
        }

        public bool HasPrev
        {
            get => hasPrev;
            set => SetProperty(ref hasPrev, value);
        }

        public bool HasNext
        {
            get => hasNext;
            set => SetProperty(ref hasNext, value);
        }

        // can't go back at initialization
        private bool hasPrev = false;

        private bool hasNext = false;
        private bool hasItemsSelected;
        private bool selectedItemsAreValid;
        private bool hasCurrentItemsSelectedAndValid = true;

        private void SelectedItemChanged()
        {
            this.HasPrev = this.PartsCollectionView != null && this.PartsCollectionView.CurrentPosition != 0;
            this.HasNext = this.PartsCollectionView != null &&
                           (this.PartsCollectionView.CurrentPosition < 0 ||
                            this.PartsCollectionView.CurrentPosition + 1 < this.PartsCollectionView.Count);

            this.OnCurrentSelectedItemChanged(false);

        }

        private void PrevPart()
        {
            this.PartsCollectionView.MoveCurrentToPrevious();

            if (this.PartsCollectionView.IsCurrentBeforeFirst)
            {
                this.PartsCollectionView.MoveCurrentToFirst();
            }

            this.SelectedItemChanged();

        }

        private void NextPart()
        {
            this.PartsCollectionView.MoveCurrentToNext();
            if (this.PartsCollectionView.IsCurrentAfterLast)
            {
                this.PartsCollectionView.MoveCurrentToLast();
            }

            this.SelectedItemChanged();
        }

        protected async Task SetupItemCommandAsync()
        {
            try
            {
                await this.SetupItemsAsync().ConfigureAwait(false);
            }
            finally
            {
                // this needs to be sync with the main thread as it is after the await portion
                await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(
                    () =>
                    {
                        this.PartsCollectionView.MoveCurrentToFirst();
                        this.SelectedItemChanged();
                    }));
            }
        }

        protected abstract Task SetupItemsAsync();

        protected abstract void ExecuteSync();

        protected abstract Task ExecuteValidateAsync();

        protected abstract Task ExecuteFetchAsync();

        protected virtual bool CanExecuteFetch()
        {
            return true;
        }

    }
}
