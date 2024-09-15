﻿using LibraryManagement.Api.Dtos.Users;
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

        /// <summary>
        /// Cadastrar um novo usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType<UserResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null) return NotFound("Usuário não encontrado");

            var response = UserResponseDto.FromEntity(user);
            return Ok(response);
        }
    }
}
