using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Application.Services
{
    public class LoanService : ILoanService
    {
        public LoanResponseDto Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<LoanResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public LoanResponseDto GetAllUserLoan(int idUser)
        {
            throw new NotImplementedException();
        }

        public LoanResponseDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public LoanResponseDto GetLoanByBookIdAndUserId(int idBookId, int userId)
        {
            throw new NotImplementedException();
        }

        public LoanResponseDto Insert(LoanRequestDto request)
        {
            throw new NotImplementedException();
        }

        public string ReturnLoan(int id, LoanReturnRequestDto request)
        {
            throw new NotImplementedException();
        }

        public LoanResponseDto Update(int id, LoanRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
