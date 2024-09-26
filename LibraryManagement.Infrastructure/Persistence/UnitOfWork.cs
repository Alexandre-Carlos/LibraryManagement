using LibraryManagement.Core.Repositories;

namespace LibraryManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryManagementDbContext _context;
        public UnitOfWork(
            LibraryManagementDbContext context,
            IBookRepository books,
            IUserRepository users,
            ILoanRepository loans
            )
        {
            _context = context;
            Books = books;
            Users = users;
            Loans = loans;
        }
        public IBookRepository Books { get; }

        public IUserRepository Users { get; }

        public ILoanRepository Loans { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
