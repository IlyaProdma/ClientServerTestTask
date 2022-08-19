using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// (проверка наличия пользователя и его учетных данных)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        /// <summary>
        /// DbContext для работы с базой пользователей
        /// </summary>
        private readonly DataAccess.LoginContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">логгер по умолчанию/param>
        public UserController(ILogger<User> logger)
        {
            _logger = logger;
            _context = new DataAccess.LoginContext();
        }

        /// <summary>
        /// Обработчик GET-запроса для проверки существования пользователя
        /// с данным логином
        /// </summary>
        /// <param name="login">логин, введенный пользователем</param>
        /// <returns>существование учетки с данным логином</returns>
        [HttpGet("{login}", Name = "CheckByLogin")]
        public IActionResult CheckByLogin(string login) =>
            DBMethods.CheckUserExists(login) == true ? Ok(login) : NotFound();

        /// <summary>
        /// Обработчик GET-запроса на авторизацию (проверка данных учетки)
        /// Берет из хедера запроса логин + MD5-код пароля, запрашивает у бд проверку
        /// </summary>
        /// <returns>код с разрешением/запрещением входа или уведомляет об отсутствии нужного хедера</returns>
        [HttpGet("/auth", Name = "Authorize")]
        public IActionResult Authorize()
        {
            if (Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                string strCreds = authHeader.First();
                var creds = strCreds.Split('_');
                return DBMethods.CheckPasswordCorrect(creds[0], creds[1]) ? Ok() : Forbid("Wrong password");
            }
            else
            {
                return BadRequest("Missing Authorization Header.");
            }
        }
    }
}