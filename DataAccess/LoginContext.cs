using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// Класс DbContext для работы с таблицей пользователей
    /// </summary>
    public class LoginContext : DbContext
    {
        /// <summary>
        /// Список пользователей из таблицы
        /// </summary>
        public DbSet<User> users { get; set; }
        
        public LoginContext()
        {
        }

        /// <summary>
        /// Конфигурация подключения к Postgres
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=testtaskdb;Username=app_;Password=123");
        }
    }
}
