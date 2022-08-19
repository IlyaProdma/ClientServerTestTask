using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    /// <summary>
    /// Класс с функциями обращения к базе данных
    /// </summary>
    public static class DBMethods
    {
        /// <summary>
        /// Запрос на проверку наличия вводимого логина
        /// </summary>
        /// <param name="login">логин для проверки существования</param>
        /// <returns></returns>
        public static bool CheckUserExists(string login)
        {
            using (var context = new LoginContext())
            {
                var user = from users in context.users
                           where users.login_.Equals(login)
                           select users;
                return user.Any();
            }
        }

        /// <summary>
        /// Запрос на проверку пароля по данному пользователю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Код пароля</param>
        /// <returns></returns>
        public static bool CheckPasswordCorrect(string login, string password)
        {
            using (var context = new LoginContext())
            {
                var user = from users in context.users
                           where users.login_.Equals(login)
                           && users.password_.Equals(password)
                           select users;
                return user.Any();
            }
        }

        /// <summary>
        /// Запрос на получение списка всех продуктов в таблице
        /// </summary>
        /// <returns>Список продуктов в таблице</returns>
        public static List<Product> GetProducts()
        {
            using (var context = new DataContext())
            {
                return (from products in context.products
                       select products).ToList<Product>();
            }
        }

        /// <summary>
        /// Запрос на получение конкретного продукта по его артикулу
        /// </summary>
        /// <param name="vendor">артикул искомого продукта</param>
        /// <returns>искомый продукт, если его артикул есть в базе</returns>
        public static Product? GetProductByVendor(string vendor)
        {
            using (var context = new DataContext())
            {
                var response = from products in context.products
                               where products.vendor_.Equals(vendor)
                               select products;
                if (response.Count() > 0)
                {
                    return response.First();
                }
                return null;
            }
        }

        /// <summary>
        /// Запрос на добавление нового продукта
        /// </summary>
        /// <param name="product">Продукт для добавления</param>
        public static void AddProduct(Product product)
        {
            using (var context = new DataContext())
            {
                    context.Entry(product).State = EntityState.Added;
                    context.SaveChanges();
            }
        }
    }
}
