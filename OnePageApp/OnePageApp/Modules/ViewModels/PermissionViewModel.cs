﻿using System;
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
    public class PermissionViewModel : BindableBase
    {
        AppSettings appSettings;
        IEventAggregator eventAggregator;
        IUserService userService;
        IChildWindowLauncher childWindowLauncher;
        IDialogCoordinator dialogCoordinator;
        IUserLdapService userLdapService;
        private DelegateCommand addUsersCommand;
        private DelegateCommand editPermissionsCommand;

        public PermissionViewModel(
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
            this.CanEditPermission = false;

            this.Users = new ObservableCollection<BaseUser>();
            this.RefreshData();
            EditPermissionsCommand = new DelegateCommand(async () => await EditPermissionsCommandAsync()).ObservesCanExecute(() => this.CanEditPermission);
            this.eventAggregator.GetEvent<RefreshDataEvent>().Subscribe(this.RefreshData, ThreadOption.UIThread, true);
        }

        public ObservableCollection<BaseUser> Users { get; set; }

        private void RefreshData(string payload = "")
        {
            this.Users.Clear();
            foreach (var item in this.userService.GetAll())
            {
                this.Users.Add(item);
            }            
        }

        private User selectedItem;
        public User SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
                this.CanEditPermission = this.selectedItem != null;
            }
        }
        
        private bool canEditPermission;
        public bool CanEditPermission
        {
            get => canEditPermission;
            set => SetProperty(ref canEditPermission, value);
        }

        public DelegateCommand AddUserCommand
        {
            get => addUsersCommand;
            set => SetProperty(ref addUsersCommand, (DelegateCommand)value);
        }

        public DelegateCommand EditPermissionsCommand
        {
            get => editPermissionsCommand;
            set => SetProperty(ref editPermissionsCommand, (DelegateCommand)value);
        }

        protected async Task EditPermissionsCommandAsync()
        {
            try
            {
                var viewModel = new EditPermissionModel(appSettings, this.userLdapService, this.SelectedItem);
                var result = await childWindowLauncher.ShowChildWindowAsync(viewModel);

                if (!result)
                {
                    await dialogCoordinator.ShowMessageAsync(this, "Failed", "You must edit the permissions!", MessageDialogStyle.Affirmative);
                    return;
                }

                //save to db
                //this.userService.UpdateUserPermissions(null);

                //udate screen                
                //this.Users.Remove(user);
                //this.Users.Add(user)
            }
            catch (Exception e)
            {
                Log.Add(e);
                await dialogCoordinator.ShowMessageAsync(this, "Failed", $"Exception: {e.Message}", MessageDialogStyle.Affirmative);
            }
        }      

    }
}