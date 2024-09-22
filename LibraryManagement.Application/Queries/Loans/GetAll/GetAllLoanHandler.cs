using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Loans.GetAll
{
    public class GetAllLoanHandler : IRequestHandler<GetAllLoanQuery, ResultViewModel<List<LoanResponseDto>>>
    {
        private readonly ILoanRepository _repository;

        public GetAllLoanHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<LoanResponseDto>>> Handle(GetAllLoanQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAll();

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }
    }
}
