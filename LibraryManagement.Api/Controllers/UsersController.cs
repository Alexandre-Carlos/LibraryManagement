﻿using LibraryManagement.Application.Commands.Users.Delete;
using LibraryManagement.Application.Commands.Users.Insert;
using LibraryManagement.Application.Commands.Users.Update;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Queries.Users.GetAll;
using LibraryManagement.Application.Queries.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastrar um novo usuário
        /// </summary>
        /// <param name="model">Payload dos dados para adição</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(InsertUserCommand request)
        {
            var response = await _mediator.Send(request);

            if (!response.IsSucess)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }


        /// <summary>
        /// Buscar informações de um usuário
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            if (!response.IsSucess)
                return BadRequest(response);

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
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllUserQuery());
            if (!response.IsSucess)
                return BadRequest(response);

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
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserCommand request)
        {
            var response = await _mediator.Send(request);
            if (!response.IsSucess)
                return BadRequest(response);

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
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            if (!response.IsSucess)
                return BadRequest(response);
            

            return NoContent();
        }
    }
}
