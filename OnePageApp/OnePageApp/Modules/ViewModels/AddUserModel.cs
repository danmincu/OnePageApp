using OnePageApp.Framework;
using OnePageApp.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageApp.Modules.ViewModels
{
    public class AddUserModel : ChildWindowViewModelBase
    {
        private readonly AppSettings appSettings;
        private IUserLdapService ldapService;
        private DelegateCommand searchUsers;
              
        public AddUserModel(AppSettings appSettings, IUserLdapService ldapService)
        {
            this.appSettings = appSettings;
            this.ldapService = ldapService;
            this.SearchResults = new ObservableCollection<BaseUser>();
            this.SearchUsers = new DelegateCommand(() => {
                this.SearchResults.Clear();
                var foundUsers = this.ldapService.SearchLogin(this.SearchString);
                foreach (var item in foundUsers)
                {
                    this.SearchResults.Add(item);
                }
            });
        }

        public ObservableCollection<BaseUser> SearchResults { get; set; }

        public DelegateCommand SearchUsers
        {
            get => searchUsers;
            set => SetProperty(ref searchUsers, (DelegateCommand)value);
        }

        private BaseUser selectedItem;
        public BaseUser SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }


        public override string GetMessageForFailure()
        {
            return "";
        }

        public override string GetTitleForFailure()
        {
            return "";
        }

        protected override void ExecuteOk()
        {

        }

        private string searchString;
        public string SearchString
        {
            get => searchString;
            set => SetProperty(ref searchString, value);
        }

    }
}
