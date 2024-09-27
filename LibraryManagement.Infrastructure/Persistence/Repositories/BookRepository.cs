using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LibraryManagementDbContext _context;

        public BookRepository(IUnitOfWork unitOfWork, LibraryManagementDbContext context = null)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<int> Add(Book book)
        {
            await _context.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return book.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Books.AnyAsync(c => c.Id ==id && !c.IsDeleted);
        }

        public async Task<Book?> GetByIdAndHasQuantity(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted && c.Quantity > 0);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task Update(Book book)
        {
            _context.Books.Update(book);
            await _unitOfWork.CompleteAsync();
        }
    }
}
