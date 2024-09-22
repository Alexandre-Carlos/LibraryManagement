using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetAllUserLoan
{
    public class GetAllUserLoanQuery : IRequest<ResultViewModel<List<LoanResponseDto>>>
    {
        public GetAllUserLoanQuery(int idUser)
        {
            IdUser = idUser;
        }

        public int IdUser { get; set; }
    }
}
