using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Product> products { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testtaskdb;Username=user_;Password=123");
        }
    }
}
