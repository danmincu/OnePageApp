using System;
using System.Collections.Generic;
using System.Linq;

namespace OnePageApp.Services.Examples
{
    public class UserLdapServiceExample : IUserLdapService
    {
        private List<BaseUser> internalList = new List<BaseUser>();

        public UserLdapServiceExample()
        {
            internalList.Add(new BaseUser { Name = "Ionut Filip", Login = "ifilip" });
            internalList.Add(new BaseUser { Name = "Dan Mincu", Login = "dmincu" });
            internalList.Add(new BaseUser { Name = "John Doe", Login = "jdoe" });
            internalList.Add(new BaseUser { Name = "Jane Smith", Login = "jsmith" });
        }

        public IEnumerable<BaseUser> SearchLogin(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return Enumerable.Empty<BaseUser>();
            }

            return this.internalList.Where(u => u.Login.ToLower().Contains(searchString.ToLower()));
        }

        public bool ValidateUser(BaseUser user)
        {
            return this.internalList.Any(u => u.Login.Equals(user.Login, StringComparison.InvariantCultureIgnoreCase) &&
                                              u.Name.Equals(user.Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}