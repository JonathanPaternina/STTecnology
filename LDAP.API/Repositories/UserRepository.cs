using LDAP.API.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LDAP.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveUser(UserInfo user)
        {
            var parameters = new[]
            {
                new SqlParameter("@Username", SqlDbType.NVarChar) { Value = user.Username },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = user.Email },
                new SqlParameter("@DisplayName", SqlDbType.NVarChar) { Value = user.DisplayName },
                new SqlParameter("@Department", SqlDbType.NVarChar) { Value = user.Department },
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = user.Title }
            };

            _context.Database.ExecuteSqlRaw("EXEC SaveUser @Username, @Email, @DisplayName, @Department, @Title", parameters);
        }
    }
}
