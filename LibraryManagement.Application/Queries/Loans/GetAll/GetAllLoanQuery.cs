using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetAll
{
    public class GetAllLoanQuery : IRequest<ResultViewModel<List<LoanResponseDto>>>
    {

    }
}
