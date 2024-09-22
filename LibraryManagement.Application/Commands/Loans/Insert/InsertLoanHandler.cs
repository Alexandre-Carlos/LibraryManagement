using LibraryManagement.Api.Configuration;
using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Application.Commands.Loans.Insert
{
    public class InsertLoanHandler : IRequestHandler<InsertLoanCommand, ResultViewModel<int>>
    {
        private readonly ILoanRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly int _returnDays;

        public InsertLoanHandler(ILoanRepository repository, IBookRepository bookRepository, IUserRepository userRepository, IOptions<ReturnDaysConfig> options)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _returnDays = options.Value.Default;
        }

        public async Task<ResultViewModel<int>> Handle(InsertLoanCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAndHasQuantity(request.IdBook);

            if (book is null) return ResultViewModel<int>.Error("Livro não encontrado ou não disponível para emprestimo!");

            var user = await _userRepository.GetById(request.IdUser);

            if (user is null) return ResultViewModel<int>.Error("Usuário não encontrado");

            var loan = request.ToEntity(_returnDays);

            book.SetLoanQuantity();

            await _bookRepository.Update(book);

            await _repository.Add(loan);

            var response = LoanResponseDto.FromEntity(loan);

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
