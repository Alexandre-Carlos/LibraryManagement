using Azure;
using Azure.Core;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Cadastrar um novo usuário
        /// </summary>
        /// <param name="model">Payload dos dados para adição</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(UserRequestDto request)
        {
            var response = _userService.Insert(request);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetById), new { response }, response.Data);
        }


        /// <summary>
        /// Buscar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var response = _userService.GetById(id);
            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }


        /// <summary>
        /// Buscar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var response = _userService.GetAll();
            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }



        /// <summary>
        /// Alterar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <param name="model">Payload dos dados para alteração</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UserRequestDto request)
        {
            var response = _userService.Update(id, request); 
            if (!response.IsSucess)
                return BadRequest(response.Message);

            return NoContent();
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
            var response = _userService.DeleteById(id);

            if (!response.IsSucess)
                return BadRequest(response.Message);
            

            return NoContent();
        }
    }
}
