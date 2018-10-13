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
    public class UsersViewModel : BindableBase
    {
        AppSettings appSettings;
        IEventAggregator eventAggregator;
        IUserService userService;
        IChildWindowLauncher childWindowLauncher;
        IDialogCoordinator dialogCoordinator;
        IUserLdapService userLdapService;
        private DelegateCommand addUsersCommand;
        private DelegateCommand deleteUserCommand;

        public UsersViewModel(
            IDialogCoordinator dialogCoordinator,
            AppSettings appSettings,
            IEventAggregator eventAggregator,
            IUserService userService,
            IUserLdapService userLdapService,
            IChildWindowLauncher childWindowLauncher)
        {
            this.appSettings = appSettings;
            this.eventAggregator = eventAggregator;
            this.userService = userService;
            this.dialogCoordinator = dialogCoordinator;
            this.userLdapService = userLdapService;
            this.childWindowLauncher = childWindowLauncher;
            this.CanAddUser = true;
            this.CanDeleteUser = false;

            this.Users = new ObservableCollection<BaseUser>(this.userService.GetAll());
            AddUserCommand = new DelegateCommand(async () => await AddUserCommandAsync());
            DeleteUserCommand = new DelegateCommand(async () => await DeleteUserCommandAsync()).ObservesCanExecute(() => this.CanDeleteUser);
        }

        public ObservableCollection<BaseUser> Users { get; set; }

        private User selectedItem;
        public User SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                this.CanDeleteUser = this.selectedItem != null;
            }
        }

        private bool canAddUser;
        public bool CanAddUser
        {
            get => canAddUser;
            set => SetProperty(ref canAddUser, value);
        }

        private bool canDeleteUser;        
        public bool CanDeleteUser
        {
            get => canDeleteUser;            
            set => SetProperty(ref canDeleteUser, value);
        }

        public DelegateCommand AddUserCommand
        {
            get => addUsersCommand;
            set => SetProperty(ref addUsersCommand, (DelegateCommand)value);
        }

        public DelegateCommand DeleteUserCommand
        {
            get => deleteUserCommand;
            set => SetProperty(ref deleteUserCommand, (DelegateCommand)value);
        }

        protected async Task AddUserCommandAsync()
        {
            try
            {
                var viewModel = new AddUserModel(appSettings, this.userLdapService);
                var result = await childWindowLauncher.ShowChildWindowAsync(viewModel);

                if (!result)
                    return;

                if (result && viewModel.SelectedItem == null)
                {
                    await dialogCoordinator.ShowMessageAsync(this, "Failed", "You must select a user!", MessageDialogStyle.Affirmative);
                    return;
                }

                var user = new User(viewModel.SelectedItem);
                //save to db
                this.userService.AddUser(user);

                //udate screen
                this.Users.Add(user);

                this.eventAggregator.GetEvent<RefreshDataEvent>().Publish("User added!");
            }
            catch(Exception e)
            {
                Log.Add(e);
                await dialogCoordinator.ShowMessageAsync(this, "Failed", $"Exception: {e.Message}", MessageDialogStyle.Affirmative);
            }
        }

        protected async Task DeleteUserCommandAsync()
        {
            try
            {
                var user = this.selectedItem;
                if (user == null)
                    return;
                try
                {
                    this.userService.DeleteUser(user);
                    this.Users.Remove(user);
                    this.eventAggregator.GetEvent<RefreshDataEvent>().Publish("User removed!");
                }
                catch (Exception e)
                {
                    Log.Add(e);
                    await dialogCoordinator.ShowMessageAsync(this, "Failed", "Cannot Delete user", MessageDialogStyle.Affirmative);
                }
            }
            finally
            {

            }
        }



    }
}
