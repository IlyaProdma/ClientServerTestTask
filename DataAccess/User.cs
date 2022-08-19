using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess
{
    /// <summary>
    /// Класс пользователя, повторяющий модель из БД
    /// </summary>
    public class User
    {
        /// <summary>
        /// Логин, первичный ключ
        /// </summary>
        [Key]
        public string login_ { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string password_ { get; set; }

        public User() { }

        /// <summary>
        /// Конструктор с возможностью JSON-сериализации/десерализации
        /// </summary>
        /// <param name="login_">логин пользователя</param>
        /// <param name="password_">MD5-код пароля</param>
        [JsonConstructor]
        public User(string login_, string password_)
        {
            this.login_ = login_;
            this.password_ = password_;
        }

        /// <summary>
        /// Метод, генерирующий MD5-код по введенной строке пароля
        /// </summary>
        /// <param name="input">сырая строка пароля</param>
        /// <returns>MD5-код введенной строки</returns>
        public static string ComputeMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }
    }
}