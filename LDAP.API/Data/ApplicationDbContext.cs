using Microsoft.EntityFrameworkCore;

namespace LDAP.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserInfo> Users { get; set; }
    }
}
