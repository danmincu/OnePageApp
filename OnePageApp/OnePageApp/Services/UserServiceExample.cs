using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageApp.Services
{
    public class UserServiceExample : IUserService
    {
        private List<User> internalList = new List<User>();

        public UserServiceExample()
        {
            internalList.Add(new User { Name = "Ionut Filip", Login = "ifilip" });
            internalList.Add(new User { Name = "Dan Mincu", Login = "dmincu" });
        }

        public void AddUser(User user)
        {
            if (!this.internalList.Any(u => u.Login.Equals(user.Login, StringComparison.InvariantCultureIgnoreCase)))
                internalList.Add(user);
            else
                throw new Exception("User already added");
        }

        public void DeleteUser(User user)
        {
            if (internalList.Contains(user))
                internalList.Remove(user);
            else
                throw new Exception("User not found");

        }

        public List<User> GetAll()
        {
            return internalList;
        }

        public void UpdateUserPermissions(User user)
        {
            var findUser = internalList.Where(u => u == user).FirstOrDefault();
            if (findUser != null)
            {
                findUser.ViewPermissions = user.ViewPermissions.ToList();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }


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
