using Azure.Core;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryManagementDbContext _context;

        public LoanRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();

            return loan.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Loans.AnyAsync(l => l.IdBook == id && l.Active);
        }

        public async Task<List<Loan>> GetAll()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active).ToListAsync();
        }

        public async Task<List<Loan>> GetAllBookByUserLoan(int idUser, int idBook)
        {
            return await _context.Loans
                .Include(l => l.Book)
            .Include(l => l.User)
            .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser && c.IdBook == idBook).ToListAsync();
        }

        public async Task<List<Loan>> GetAllUserLoan(int idUser)
        {
            return await _context.Loans
            .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser).ToListAsync();
        }

        public async Task<Loan?> GetById(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.Id == id && l.Active);
        }

        public async Task Update(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
