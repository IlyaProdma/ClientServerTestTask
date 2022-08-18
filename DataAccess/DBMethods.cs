using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DBMethods
    {
        public static bool CheckUserExists(LoginContext context, string login)
        {
            using (context)
            {
                var user = from users in context.users
                           where users.login_.Equals(login)
                           select users;
                return user.Any();
            }
        }

        public static bool CheckPasswordCorrect(LoginContext context, string login, string password)
        {
            using (context)
            {
                var user = from users in context.users
                           where users.login_.Equals(login)
                           && users.password_.Equals(password)
                           select users;
                return user.Any();
            }
        }

        public static List<Product> GetProducts(DataContext context)
        {
            using (context)
            {
                return (from products in context.products
                       select products).ToList<Product>();
            }
        }

        public static Product GetProduct(DataContext context)
        {
            using (context)
            {
                return (from products in context.products
                        select products).First();
            }
        }

        public static void AddProduct(DataContext context, Product product)
        {
            using (context)
            {
                context.Entry(product).State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
