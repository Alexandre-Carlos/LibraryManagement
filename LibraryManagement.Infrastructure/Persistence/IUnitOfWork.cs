using LibraryManagement.Core.Repositories;
using System.Diagnostics;

namespace LibraryManagement.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();

    }
}
