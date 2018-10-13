using System.Collections.Generic;

namespace OnePageApp.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        void DeleteUser(User user);
        void AddUser(User user);
        void UpdateUserPermissions(User user);
    }

    public interface IUserLdapService
    {
        IEnumerable<BaseUser> SearchLogin(string userLogin);
        bool ValidateUser(BaseUser user);
    }


    public class BaseUser
    {
        public string Login { set; get; }
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseUser))
                return false;

            var other = obj as BaseUser;

            if (Login != other.Login)
                return false;

            return true;
        }


        public static bool operator ==(BaseUser x, BaseUser y)
        {
            if (ReferenceEquals(x,null) && ReferenceEquals(y, null))
                return true;
            if (ReferenceEquals(x, null)) return false;
            return x.Equals(y);
        }

        public static bool operator !=(BaseUser x, BaseUser y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return this.Login.GetHashCode();
        }

    }

    public class User : BaseUser
    {
        public User() { }
        public User(BaseUser user)
        {
            this.Name = user.Name;
            this.Login = user.Login;
            this.ViewPermissions = new List<BaseOrganization>();
        }

        public List<BaseOrganization> ViewPermissions { set; get; }      
    }

    public class BaseOrganization
    {
        public bool IsSelected { get; set; }
        public int Level { get; }
        public int Code { get; set; }
        public string Name { get; set; }
        public List<BaseOrganization> Items { get; set; }
    }

    public class Portofolio : BaseOrganization
    {
        public int Level
        {
            get { return 0; }
        }
    }

    public class Branch : BaseOrganization
    {
        public int Level
        {
            get { return 1; }
        }
    }

    public class Unit : BaseOrganization
    {
        public int Level
        {
            get { return 2; }
        }
    }


}
