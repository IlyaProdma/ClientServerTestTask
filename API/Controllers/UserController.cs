using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        private readonly DataAccess.LoginContext _context;

        public UserController(ILogger<User> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _context = new DataAccess.LoginContext();
        }

        [HttpGet("{login}", Name = "CheckByLogin")]
        public IActionResult CheckByLogin(string login) =>
            DBMethods.CheckUserExists(_context, login) == true ? Ok(login) : NotFound();

        [HttpGet("/auth", Name = "Authorize")]
        public IActionResult Authorize()
        {
            if (Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                string strCreds = authHeader.First();
                var creds = strCreds.Split('_');
                return DBMethods.CheckPasswordCorrect(_context, creds[0], creds[1]) ? Ok() : Forbid("Wrong password");
            }
            else
            {
                return BadRequest("Missing Authorization Header.");
            }
        }
    }
}