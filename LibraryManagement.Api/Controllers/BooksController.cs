using LibraryManagement.Application.Commands.Books.Delete;
using LibraryManagement.Application.Commands.Books.Insert;
using LibraryManagement.Application.Commands.Books.Update;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Application.Queries.Books.GetAll;
using LibraryManagement.Application.Queries.Books.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Listar todos os livros
        /// </summary>
        /// <returns>Lista de Livros</returns>
        [HttpGet]
        [ProducesResponseType<BookResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllBooksQuery());

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Listar um livro através do identificador
        /// </summary>
        /// <param name="id">Identificador do Livro</param>
        /// <returns>Dados do Livro</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<BookResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetBookByIdQuery(id));

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Adicionar um novo livro
        /// </summary>
        /// <param name="model">Payload dos dados para adição</param>
        /// <returns>Informações do livro adicionado</returns>
        [HttpPost]
        [ProducesResponseType<BookResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] InsertBookCommand request)
        {
            var response = await _mediator.Send(request);

            if (!response.IsSuccess)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById),new { id = response.Data }, response);
        }

        /// <summary>
        /// Alterar informações de um livro
        /// </summary>
        /// <param name="id">Identificador do Livro</param>
        /// <param name="model">Payload dos dados para alteração</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBookCommand request)
        {
            var response = await _mediator.Send(request);

            if (!response.IsSuccess)
               return BadRequest(response);

            return NoContent();
        }

        /// <summary>
        /// Excluir um livro
        /// </summary>
        /// <param name="id">Identificador do Livro</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<string>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteBookCommand(id));

            if (!response.IsSuccess)
                return BadRequest(response);

            return NoContent();
        }
    }
}
