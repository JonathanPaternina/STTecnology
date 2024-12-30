namespace LDAP.API.Services
{
    public interface ILDAPService
    {
        bool AuthenticateUser(string username, string password);
    }
}
