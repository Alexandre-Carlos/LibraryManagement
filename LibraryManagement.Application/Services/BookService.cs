using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Infrastructure.Persistence;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryManagementDbContext _context;

        public BookService(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public ResultViewModel Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            var loan = _context.Loans.Any(l => l.IdBook == id && l.Active);

            if (loan)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            book.SetAsDeleted();

            _context.Books.Update(book);
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<BookResponseDto>> GetAll()
        {
            var books = _context.Books.Where(c => !c.IsDeleted).ToList();

            var response = books.Select(b => BookResponseDto.FromEntity(b)).ToList();

            return ResultViewModel<List<BookResponseDto>>.Sucess(response);
        }

        public ResultViewModel<BookResponseDto> GetById(int id)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);

            if (book is null) return ResultViewModel<BookResponseDto>.Error("Livro não encontrado");

            var response = BookResponseDto.FromEntity(book);

            return ResultViewModel<BookResponseDto>.Sucess(response);
        }

        public ResultViewModel<int> Insert(BookRequestDto request)
        {
            var book = request.ToEntity();

            _context.Books.Add(book);
            _context.SaveChanges();

            var response = BookResponseDto.FromEntity(book);

            return ResultViewModel<int>.Sucess(response.Id);
        }

        public ResultViewModel Update(int id, BookUpdateRequestDto request)
        {
            var book = _context.Books.SingleOrDefault(c => c.Id == id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            book.SetTitle(request.Title);
            book.SetAuthor(request.Author);
            book.SetIsbn(request.Isbn);
            book.SetYearPublished(request.YearPublished);

            _context.Books.Update(book);
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
