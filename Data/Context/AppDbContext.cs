using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

    }
}
