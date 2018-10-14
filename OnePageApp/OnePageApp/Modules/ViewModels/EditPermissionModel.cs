using System;
using OnePageApp.Framework;
using OnePageApp.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace OnePageApp.Modules.ViewModels
{
    public class EditPermissionModel : ChildWindowViewModelBase
    {
        private readonly AppSettings appSettings;
        private IPermissions permissionsService;

        public EditPermissionModel(AppSettings appSettings, IPermissions permissionsService, User user)
        {
            this.appSettings = appSettings;
            this.permissionsService = permissionsService;
            this.User = user;
            this.PermissionTree = permissionsService.GetPermissionTree().ToList();
            SetSelectionBaseOnPermissions(this.User.ViewPermissions, this.PermissionTree);
        }

        private User user;
        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        public List<BasePermission> PermissionTree { get; set; }


        public override string GetMessageForFailure()
        {
            return "";
        }

        public override string GetTitleForFailure()
        {
            return "";
        }

        private void CalculateEffectivePermissions(List<BasePermission> effective_permissions, IEnumerable<BasePermission> source)
        {
            foreach (var baseOrganization in source)
            {
                if (baseOrganization.Items == null || !baseOrganization.Items.Any())
                {
                    if (baseOrganization.IsSelected)
                        effective_permissions.Add(baseOrganization);
                }
                else
                {
                    CalculateEffectivePermissions(effective_permissions, baseOrganization.Items);
                }
            }
        }

        private void SetSelectionBaseOnPermissions(List<BasePermission> effective_permissions,
            IEnumerable<BasePermission> source)
        {
            if (effective_permissions == null)
                return;

            foreach (var baseOrganization in source)
            {
                System.Diagnostics.Trace.WriteLine(baseOrganization.Name);
                if (baseOrganization.Items == null || !baseOrganization.Items.Any())
                {
                    if (effective_permissions.Contains(baseOrganization))
                        baseOrganization.IsSelected = true;
                }
                else
                {
                    SetSelectionBaseOnPermissions(effective_permissions, baseOrganization.Items);
                }
            }
        }

        protected override void ExecuteOk()
        {
            var effective_permissions = new List<BasePermission>();
            this.CalculateEffectivePermissions(effective_permissions, this.PermissionTree);
            this.User.ViewPermissions = effective_permissions;
        }

    }
}
