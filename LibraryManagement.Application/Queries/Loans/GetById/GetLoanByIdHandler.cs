using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Application.Queries.Loans.GetById
{
    public class GetLoanByIdHandler : IRequestHandler<GetLoanByIdQuery, ResultViewModel<LoanResponseDto>>
    {
        private readonly ILoanRepository _repository;

        public GetLoanByIdHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<LoanResponseDto>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetById(request.Id);

            if (loan is null) return ResultViewModel<LoanResponseDto>.Error("Emprestimo não localizado");

            var response = LoanResponseDto.FromEntity(loan);

            return ResultViewModel<LoanResponseDto>.Sucess(response);
        }
    }
}
