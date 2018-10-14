using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace OnePageApp.Services
{
    public class User : BaseUser
    {
        public User() { }
        public User(BaseUser user)
        {
            this.Name = user.Name;
            this.Login = user.Login;
            this.ViewPermissions = new List<BasePermission>();
        }

        private List<BasePermission> viewPermissions;
        public List<BasePermission> ViewPermissions
        {
            get => viewPermissions;
            set
            {
                SetProperty(ref viewPermissions, value);
                RaisePropertyChanged(nameof(User.FlatPermissions));
            }
        }

        public string FlatPermissions
        {
            get
            {
                if (this.viewPermissions == null)
                    return string.Empty;
                var list = new List<string>();
                foreach (var baseOrganization in this.ViewPermissions)
                {
                    list.Add(string.Join("-",baseOrganization.PermissionPath().Select(i => i.Name.ToString())));
                }
                return string.Join(";", list);
            }
        }

    }
}