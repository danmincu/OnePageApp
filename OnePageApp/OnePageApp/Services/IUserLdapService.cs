using System.Collections.Generic;

namespace OnePageApp.Services
{
    public interface IUserLdapService
    {
        IEnumerable<BaseUser> SearchLogin(string userLogin);
        bool ValidateUser(BaseUser user);
    }
}