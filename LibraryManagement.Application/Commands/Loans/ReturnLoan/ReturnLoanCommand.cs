using LibraryManagement.Application.Dtos;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans.ReturnLoan
{
    public class ReturnLoanCommand : IRequest<ResultViewModel<string>>
    {
        public int IdLoan { get; set; }
        public int IdUser { get; set; }
        public int IdBook { get; set; }
    }
}
