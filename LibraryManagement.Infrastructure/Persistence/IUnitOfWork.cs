using LibraryManagement.Core.Repositories;

namespace LibraryManagement.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        ILoanRepository Loans { get; }
        Task<int> CompleteAsync();

    }
}
