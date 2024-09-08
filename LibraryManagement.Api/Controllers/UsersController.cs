using LibraryManagement.Api.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(UserRequestDto model)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Login(int id)
        {
            return Ok();
        }
    }
}
