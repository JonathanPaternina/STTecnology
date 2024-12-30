using Novell.Directory.Ldap;

namespace LDAP.API.Services
{
    public class LDAPService: ILDAPService
    {
        private readonly IConfiguration _configuration;

        public LDAPService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AuthenticateUser(string username, string password)
        {
            var ldapServer = _configuration["LdapSettings:LdapServer"];
            var ldapAdminUsername = _configuration["LdapSettings:LdapAdminUsername"];
            var ldapAdminPassword = _configuration["LdapSettings:LdapAdminPassword"];

            using var ldapConnection = new LdapConnection();
            try
            {
                ldapConnection.Connect(ldapServer, LdapConnection.DefaultPort);
                ldapConnection.Bind(LdapConnection.LdapV3, ldapAdminUsername, ldapAdminPassword);

                //var userDn = $"uid={username},ou=users,dc=example,dc=com";
                //ldapConnection.Bind(LdapConnection.LdapV3, userDn, password);

                return true; // Usuario autenticado
            }
            catch (LdapException)
            {
                return false; // Error de autenticación
            }
        }
    }
}
