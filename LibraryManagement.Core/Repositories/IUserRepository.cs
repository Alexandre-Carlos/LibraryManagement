using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task Update(User user);
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<bool> Exist(int id);
        Task<User?> GetByEmail(string email);
    }
}
