using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetById
{
    public class GetLoanByIdQuery : IRequest<ResultViewModel<LoanResponseDto>>
    {
        public GetLoanByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
