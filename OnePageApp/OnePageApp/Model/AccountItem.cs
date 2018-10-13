using Prism.Mvvm;
using System;

namespace OnePageApp.Model
{
    public class AccountItem : BindableBase, ISelectable
    {
        private bool isSelected;
        private int number;
        private string name;
        private string orderDate;
        private decimal? total;
        private bool isImported;
        private string status;
        private DateTime? lastSyncDate;
        private int? syncCount;

        public int Id { get; set; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
                OnSelectionChanged?.Invoke(value);
            }
        }

        public int Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string OrderDate
        {
            get => orderDate;
            set => SetProperty(ref orderDate, value);
        }

        public decimal? Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        public bool IsImported
        {
            get => isImported;
            set => SetProperty(ref isImported, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public DateTime? LastSyncDate
        {
            get => lastSyncDate;
            set => SetProperty(ref lastSyncDate, value);
        }

        public int? SyncCount
        {
            get => syncCount;
            set => SetProperty(ref syncCount, value);
        }

        public Action<bool> OnSelectionChanged { get; set; }
    }
}
