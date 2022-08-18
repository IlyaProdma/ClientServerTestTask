using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        private readonly DataAccess.DataContext _dataContext;
        private readonly DataAccess.LoginContext _loginContext;

        public DataController(ILogger<User> logger)
        {
            _logger = logger;
            _dataContext = new DataAccess.DataContext();
            _loginContext = new DataAccess.LoginContext();
        }

        [HttpGet("products", Name = "GetProducts")]
        public IActionResult Get()
        {
            var result = DBMethods.GetProducts(_dataContext);
            if (result != null)
            {
                _logger.Log(LogLevel.Information, "not cringe");
                return new ObjectResult(result);
            }
            else
            {
                _logger.Log(LogLevel.Error, "cringe");
                return StatusCode(520);
            }
        }

        [HttpGet("products/1", Name = "GetProduct")]
        public IActionResult GetProduct()
        {
            var result = DBMethods.GetProduct(_dataContext);
            if (result != null)
            {
                _logger.Log(LogLevel.Information, "not cringe");
                return new ObjectResult(result);
            }
            else
            {
                _logger.Log(LogLevel.Error, "cringe");
                return Forbid("cringe");
            }
        }

        
        [HttpPost("new", Name = "AddProduct")]
        public IActionResult Post([FromBody] Product product)
        {
            if (Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
            {
                string strCreds = authHeader.First();
                var creds = strCreds.Split('_');
                if (DBMethods.CheckPasswordCorrect(_loginContext, creds[0], creds[1]))
                {
                    DBMethods.AddProduct(_dataContext, product);
                    return Ok(product);
                }
            }
            return BadRequest("No authorization header");
        }
    }
}
