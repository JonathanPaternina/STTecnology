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
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SaveUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    command.Parameters.Add(new SqlParameter("@Username", user.Username));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@DisplayName", user.DisplayName));
                    command.Parameters.Add(new SqlParameter("@Department", user.Department));
                    command.Parameters.Add(new SqlParameter("@Title", user.Title));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
