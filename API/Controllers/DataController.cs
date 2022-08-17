using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ILogger<User> _logger;

        private readonly DataAccess.AppContext _context;

        public DataController(ILogger<User> logger)
        {
            _logger = logger;
            _context = new DataAccess.AppContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return null;
        }

        [HttpPost]
        public IActionResult Post()
        {
            return null;
        }
    }
}
