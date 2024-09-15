using LibraryManagement.Api.Dtos.Books;
using LibraryManagement.Api.Entities;
using LibraryManagement.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;

        public BooksController(LibraryManagementDbContext context)
        {
            _context = context;
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
            var books = _context.Books.Where(c => !c.IsDeleted).ToList();

            var response = books.Select(b => BookResponseDto.FromEntity(b));

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
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book is null) return NotFound("Livro não encontrado");

            var response = BookResponseDto.FromEntity(book);

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
            var book = model.ToEntity();

            _context.Books.Add(book);
            _context.SaveChanges();

            var response = BookResponseDto.FromEntity(book);

            return CreatedAtAction(nameof(GetById),new { response.Id }, response);
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
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book is null) return NotFound();

            book.SetTitle(model.Title);
            book.SetAuthor(model.Author);
            book.SetIsbn(model.Isbn);
            book.SetYearPublished(model.YearPublished);

            _context.Books.Update(book);
            _context.SaveChanges();

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
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book is null) return NotFound();

            var loan = _context.Loans.Any(l => l.IdBook == id && l.Active);

            if (loan)
                return BadRequest("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            book.SetAsDeleted();

            _context.Books.Update(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
