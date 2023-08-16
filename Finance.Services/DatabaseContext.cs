using Finance.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Finance.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AmountConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Amount> Amounts { get; set; }
        public DbSet<Log> Logs { get; set; }


    }
}
