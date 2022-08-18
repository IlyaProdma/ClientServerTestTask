using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LoginContext : DbContext
    {
        public DbSet<User> users { get; set; }
        
        public LoginContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testtaskdb;Username=app_;Password=123");
        }
    }
}
