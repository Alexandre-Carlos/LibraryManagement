using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetAllUserLoan
{
    public class GetAllUserLoanHandler : IRequestHandler<GetAllUserLoanQuery, ResultViewModel<List<LoanResponseDto>>>
    {
        private readonly ILoanRepository _repository;

        public GetAllUserLoanHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<LoanResponseDto>>> Handle(GetAllUserLoanQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllUserLoan(request.IdUser);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }
    }
}
