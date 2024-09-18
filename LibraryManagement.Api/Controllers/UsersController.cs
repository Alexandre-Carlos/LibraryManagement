using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Cadastrar um novo usuário
        /// </summary>
        /// <param name="model">Payload dos dados para adição</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(UserRequestDto model)
        {
            var response = model.ToEntity();

            _context.Users.Add(response);
            _context.SaveChanges();

            return Ok("Usuário criado com Sucesso!");
        }


        /// <summary>
        /// Buscar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Id == id);

            
            if (user is null) return NotFound("Usuário não encontrado");

            var response = UserResponseDto.FromEntity(user);
            return Ok(response);
        }

        /// <summary>
        /// Alterar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="model">Payload dos dados para alteração</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UserRequestDto model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null) return NotFound("Usuário não encontrado");

            user.SetName(model.Name);   
            user.SetEmail(model.Email);

            _context.Users.Update(user);
            _context.SaveChanges();

            var response = UserResponseDto.FromEntity(user);
            return Ok(response);
        }

        /// <summary>
        /// Excluir um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id==id);

            if (user is null) return NotFound("Usuário não encontrado");

            var loans = _context.Loans.Any(x => x.IdUser == id && x.Active);

            if (loans)
                return BadRequest("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            user.SetAsDeleted();
            _context.SaveChanges();

            return NoContent();
        }
    }
}
