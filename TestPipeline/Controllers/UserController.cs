using Microsoft.AspNetCore.Mvc;

using TestPipeline.Models;
using TestPipeline.Services.Abstractions;

namespace TestPipeline.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserQueryService userQueryService;

        
        public UserController(ILogger<UserController> logger, IUserQueryService userQueryService)
        {
            _logger = logger;
            this.userQueryService = userQueryService;
        }

        [Route("get-user-by-email")]
        [HttpGet]
        public User GetUserByEmail(string email)
        {
            var u = userQueryService.GetUserByEmail(email);
            return u;
        }

        [Route("get-user-by-id")]
        [HttpGet]
        public User GetUserById(int userId)
        {
            var u = userQueryService.GetUserById(userId);
            return u;
        }
    }
}