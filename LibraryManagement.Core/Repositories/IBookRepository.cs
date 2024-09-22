using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface IBookRepository
    {
        Task<int> Add(Book book);
        Task Update(Book book);
        Task<List<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task<bool> Exists(int id);
        Task<Book?> GetByIdAndHasQuantity(int id);
    }
}
