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
}
