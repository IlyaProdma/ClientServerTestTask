using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DBMethods
    {
        public static bool CheckUserExists(AppContext context, string login)
        {
            using (context)
            {
                var user = from users in context.users
                           where users.login_.Equals(login)
                           select users;
                return user.Any();
            }
        }

        public static bool CheckPasswordCorrect(AppContext context, string login, string password)
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

        public static void AddNewUser(AppContext context, User user)
        {
            using (context)
            {
                context.users.Add(user);
            }
        }

        public static User? GetUserByLogin(AppContext context, string login)
        {
            using (context)
            {
                try
                {
                    var user = (from users in context.users
                                where users.login_.Equals(login)
                                select users).Take(1).First();
                    return user;
                } catch
                {
                    return null;
                }
            }
        }
    }
}
