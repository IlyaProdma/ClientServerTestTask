using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для работы с данными в таблице продуктов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        /// <summary>
        /// DbContext для работы с таблицей продуктов
        /// </summary>
        private readonly DataAccess.DataContext _dataContext;

        /// <summary>
        /// DbContext для работы с таблицей пользователей (авторизация)
        /// </summary>
        private readonly DataAccess.LoginContext _loginContext;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">логгер по умолчанию</param>
        public DataController(ILogger<User> logger)
        {
            _logger = logger;
            _dataContext = new DataAccess.DataContext();
            _loginContext = new DataAccess.LoginContext();
        }

        /// <summary>
        /// Обработчик GET-запроса на получение списка всех продуктов в таблице
        /// </summary>
        /// <returns>JSON со списком или Unknown Error (не придумал, какая лучше подойдет)</returns>
        [HttpGet("products", Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            var result = DBMethods.GetProducts();
            if (result != null)
            {
                return new ObjectResult(result);
            }
            else
            {
                return StatusCode(520);
            }
        }

        /// <summary>
        /// Обработчик GET-запроса на получение конкретного продукта по артикула
        /// </summary>
        /// <param name="vendor">Артикул искомого продукта</param>
        /// <returns>JSON с объектом продукта или Unknown Error</returns>
        [HttpGet("products/{vendor}", Name = "GetProduct")]
        public IActionResult GetProduct(string vendor)
        {
            var result = DBMethods.GetProductByVendor(vendor);
            if (result != null)
            {
                return new ObjectResult(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Обработчик POST-запроса на добавление нового продукта
        /// </summary>
        /// <param name="product">Добавляемый продукт</param>
        /// <returns>Код, определяющий итог выполнения задачи</returns>
        [HttpPost("products/new", Name = "AddProduct")]
        public IActionResult Post([FromBody] Product product)
        {
            if (Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                string strCreds = authHeader.First();
                var creds = strCreds.Split('_');
                if (DBMethods.CheckPasswordCorrect(creds[0], creds[1]))
                {
                    try
                    {
                        if (DBMethods.GetProductByVendor(product.vendor_) != null)
                        {
                            return BadRequest("Product with this vendor is already added");
                        }
                        else
                        {
                            DBMethods.AddProduct(product);
                            return Ok(product);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(LogLevel.Error, ex.Message);
                        if (ex.InnerException != null)
                        {
                            _logger.Log(LogLevel.Error, ex.InnerException.Message);
                        }
                        return BadRequest(ex.Message);
                    }
                }
            }
            return BadRequest("No authorization header");
        }
    }
}
