using Microsoft.EntityFrameworkCore;

namespace LDAP.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .HasNoKey();
        }
        public DbSet<UserInfo> Users { get; set; }
    }
}
