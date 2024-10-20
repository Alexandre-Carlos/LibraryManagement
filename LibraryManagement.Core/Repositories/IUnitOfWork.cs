namespace LibraryManagement.Core.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();

    }
}