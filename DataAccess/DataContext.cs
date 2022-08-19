using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// Класс DbContext для работы с таблицей продуктов
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Список продуктов из таблицы
        /// </summary>
        public DbSet<Product> products { get; set; }

        public DataContext()
        {            
        }

        /// <summary>
        /// Конфигурация подключения к Postgres
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testtaskdb;Username=user_;Password=123");
        }
    }
}
