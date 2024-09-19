using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Application.Services
{
    public interface ILoanService
    {
        ResultViewModel<List<LoanResponseDto>> GetAll();
        ResultViewModel<LoanResponseDto> GetById(int id);
        ResultViewModel<int> Insert(LoanRequestDto request);
        ResultViewModel<string> ReturnLoan(int id, LoanReturnRequestDto request);
        ResultViewModel<List<LoanResponseDto>> GetAllUserLoan(int idUser);
        ResultViewModel<List<LoanResponseDto>> GetLoanByBookIdAndUserId(int bookId, int userId);
    }
}
