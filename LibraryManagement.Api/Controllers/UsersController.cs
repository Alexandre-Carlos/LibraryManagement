using LibraryManagement.Api.Dtos.Users;
using LibraryManagement.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;

        public UsersController(LibraryManagementDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(UserRequestDto model)
        {
            var response = model.ToEntity();

            _context.Users.Add(response);
            _context.SaveChanges();

            return Ok("Usuário criado com Sucesso!");
        }

        [HttpGet("{id}")]
        public IActionResult Login(int id)
        {
            return Ok();
        }
    }
}
