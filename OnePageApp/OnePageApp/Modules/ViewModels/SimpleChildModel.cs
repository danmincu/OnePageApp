using System.Collections.ObjectModel;
using OnePageApp.Framework;

namespace OnePageApp.Modules.ViewModels
{
    public class SimpleChildModel : ChildWindowViewModelBase
    {
        private readonly AppSettings appSettings;
        private Item selectedItem;

        public ObservableCollection<Item> Items { get; set; }

        public Item SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public SimpleChildModel(AppSettings appSettings)
        {
            this.appSettings = appSettings;
            Items = new ObservableCollection<Item>(new[]{new Item{ Name = "A", Value = "A"}, new Item{ Name ="B", Value = "B"}} );
        }
        protected override void ExecuteOk()
        {
            if (SelectedItem != null)
            {

            }
        }

        public override string GetTitleForFailure()
        {
            return "Item failed";
        }

        public override string GetMessageForFailure()
        {
            return "You must select your default Item.";
        }

        public class Item
        {
            public string Name { set; get; }
            public string Value { set; get; }
        }
    }

}
