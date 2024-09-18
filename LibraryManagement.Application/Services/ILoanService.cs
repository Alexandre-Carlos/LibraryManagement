using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Application.Services
{
    public interface ILoanService
    {
        List<LoanResponseDto> GetAll();
        LoanResponseDto GetById(int id);
        LoanResponseDto Insert(LoanRequestDto request);
        LoanResponseDto Update(int id, LoanRequestDto request);
        LoanResponseDto Delete(int id);
        string ReturnLoan(int id, LoanReturnRequestDto request);
        LoanResponseDto GetAllUserLoan(int idUser);
        LoanResponseDto GetLoanByBookIdAndUserId(int idBookId, int userId);
    }
}
