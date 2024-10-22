using LibraryManagement.Application.Configuration;
using LibraryManagement.Application.Services.Authorize;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationConfig _appConfig;
        public AuthController(ApplicationConfig appConfig)
        {
            _appConfig = appConfig;
        }


        [HttpPost]
       public IActionResult Auth(string username, string password)
        {
            if (username == "Alexandre" && password == "123456")
            {
                var token = AuthenticationConfig.GenerateToken(new User(username, "alexandrecarlos2@gmail.com"), _appConfig);
                return Ok(token);
            }

            return BadRequest("UserNAme or password invalid");
        }
    }
}
