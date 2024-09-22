using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface ILoanRepository
    {
        Task<int> Add(Loan loan);
        Task Update(Loan loan);
        Task<List<Loan>> GetAll();
        Task<Loan?> GetById(int id);
        Task<bool> Exists(int id);
        Task<List<Loan>> GetAllUserLoan(int idUser);
        Task<List<Loan>> GetAllBookByUserLoan(int idUser, int idBook);

    }
}
