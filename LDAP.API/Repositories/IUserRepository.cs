using LDAP.API.Data;

namespace LDAP.API.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(UserInfo user);
    }
}
