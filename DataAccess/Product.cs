using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class Product
    {
        [Key]
        public string vendor_ { get; set; }
        public string name_ { get; set; }
        public int price_ { get; set; }
        public string? description_ { get; set; }

        [JsonConstructor]
        public Product(string vendor_, string name_, int price_, string description_)
        {
            this.vendor_ = vendor_;
            this.name_ = name_;
            this.price_ = price_;
            this.description_ = description_;
        }
    }
}
