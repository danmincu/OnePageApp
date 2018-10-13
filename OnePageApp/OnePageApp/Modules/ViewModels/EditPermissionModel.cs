using OnePageApp.Framework;
using OnePageApp.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnePageApp.Modules.ViewModels
{
    public class EditPermissionModel : ChildWindowViewModelBase
    {
        private readonly AppSettings appSettings;
        private IUserLdapService ldapService;
        private DelegateCommand searchUsers;
              
        public EditPermissionModel(AppSettings appSettings, IUserLdapService ldapService, User user)
        {
            this.appSettings = appSettings;
            this.ldapService = ldapService;
            this.User = user;
            this.SearchResults = new ObservableCollection<BaseUser>();
            this.SearchUsers = new DelegateCommand(() => {});
        }

        public ObservableCollection<BaseUser> SearchResults { get; set; }

        public DelegateCommand SearchUsers
        {
            get => searchUsers;
            set => SetProperty(ref searchUsers, (DelegateCommand)value);
        }

        private BaseUser user;
        public BaseUser User
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        public List<BaseOrganization> PermissionTree { get; set; }


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

    }
}
