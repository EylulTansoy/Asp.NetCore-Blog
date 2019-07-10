using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

    }
}
