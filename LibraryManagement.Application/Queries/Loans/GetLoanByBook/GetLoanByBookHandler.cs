using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetLoanByBook
{
    public class GetLoanByBookHandler : IRequestHandler<GetLoanByBookQuery, ResultViewModel<List<LoanResponseDto>>>
    {
        private readonly ILoanRepository _repository;

        public GetLoanByBookHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<LoanResponseDto>>> Handle(GetLoanByBookQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllBookByUserLoan(request.UserId, request.BookId);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }
    }
}
