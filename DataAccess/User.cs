using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess
{
    public class User
    {
        [Key]
        public string login_ { get; set; }
        public string password_ { get; set; }

        public User() { }

        [JsonConstructor]
        public User(string login_, string password_)
        {
            this.login_ = login_;
            this.password_ = password_;
        }
    }
}