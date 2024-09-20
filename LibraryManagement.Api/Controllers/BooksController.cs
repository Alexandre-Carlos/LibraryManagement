using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Listar todos os livros
        /// </summary>
        /// <returns>Lista de Livros</returns>
        [HttpGet]
        [ProducesResponseType<BookResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var response = _bookService.GetAll();

            if (!response.IsSucess)
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
        public IActionResult GetById(int id)
        {
            var response = _bookService.GetById(id);

            if (!response.IsSucess)
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
        public IActionResult Post([FromBody] BookRequestDto model)
        {
            var response = _bookService.Insert(model);

            if (!response.IsSucess)
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
        public IActionResult Put(int id, [FromBody] BookUpdateRequestDto model)
        {
            var response = _bookService.Update(id, model);

            if (!response.IsSucess)
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
        public IActionResult Delete(int id)
        {
            var response =_bookService.Delete(id);

            if (!response.IsSucess)
                return BadRequest(response);

            return NoContent();
        }
    }
}
