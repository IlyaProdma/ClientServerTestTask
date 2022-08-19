using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    /// <summary>
    /// Класс продукта, повторяющий модель из БД
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Артикул, первичный ключ
        /// </summary>
        [Key]
        public string vendor_ { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        public string name_ { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int price_ { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? description_ { get; set; }

        public Product() { }

        /// <summary>
        /// Конструктор с возможностью JSON-сериализации/десериализации
        /// </summary>
        /// <param name="vendor_">Артикул</param>
        /// <param name="name_">Название товара</param>
        /// <param name="price_">Цена</param>
        /// <param name="description_">Описание</param>
        [JsonConstructor]
        public Product(string vendor_, string name_, int price_, string description_)
        {
            this.vendor_ = vendor_;
            this.name_ = name_;
            this.price_ = price_;
            this.description_ = description_;
        }

        /// <summary>
        /// Статический метод проверки введенных данных для создания нового продукта
        /// </summary>
        /// <param name="strVendor">Введенное значение артикула</param>
        /// <param name="strName">Введенное значение имени</param>
        /// <param name="strPrice">Введенное значение цены</param>
        /// <param name="strDescription">Введенное описание</param>
        /// <returns>Новый объект продукта, если все ок; null - иначе</returns>
        public static Product? CheckInputProperties(string strVendor, string strName,
                                                   string strPrice, string strDescription)
        {
            if (!String.IsNullOrWhiteSpace(strVendor) &&
                !String.IsNullOrWhiteSpace(strName) &&
                !String.IsNullOrWhiteSpace(strPrice) &&
                Int32.TryParse(strPrice, out int price) &&
                price > 0) {
                return new Product(strVendor, strName, price, strDescription);
            }
            return null;
        }
    }
}
