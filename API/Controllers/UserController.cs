using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        private readonly DataAccess.AppContext _context;

        public UserController(ILogger<User> logger)
        {
            _logger = logger;
            _context = new DataAccess.AppContext();
        }

        [HttpGet("{login}", Name = "GetByLogin")]
        public IActionResult GetByLogin(string login)
        {
            var user = DBMethods.GetUserByLogin(_context, login);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpPost(Name = "Post")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Added;
                _context.SaveChanges();

                return Ok(user);
            } catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                if (ex.InnerException != null)
                    _logger.Log(LogLevel.Error, ex.InnerException.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}