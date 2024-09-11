using LibraryManagement.Api.Dtos.Books;
using LibraryManagement.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.Where(c => !c.IsDeleted).ToList();

            var response = books.Select(b => BookResponseDto.FromEntity(b));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book is null) return NotFound();

            var response = BookResponseDto.FromEntity(book);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookRequestDto model)
        {
            var book = model.ToEntity();

            _context.Books.Add(book);
            _context.SaveChanges();

            var response = BookResponseDto.FromEntity(book);

            return CreatedAtAction(nameof(GetById),new { response.Id }, response);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book is null) return NotFound();

            book.SetAsDeleted();

            _context.Books.Update(book);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
