using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryManagementDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(LibraryManagementDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return user.Id;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }
    }
}
