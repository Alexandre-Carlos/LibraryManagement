using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetLoanByBook
{
    public class GetLoanByBookQuery : IRequest<ResultViewModel<List<LoanResponseDto>>>
    {
        public GetLoanByBookQuery(int bookId, int userId)
        {
            BookId = bookId;
            UserId = userId;
        }

        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
