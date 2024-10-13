using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans.ReturnLoan
{
    public class ReturnLoanHandler : IRequestHandler<ReturnLoanCommand, ResultViewModel<string>>
    {
        private readonly ILoanRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnLoanHandler(ILoanRepository repository, 
            IUserRepository userRepository, 
            IBookRepository bookRepository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<string>> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var existsUser = await _userRepository.Exist(request.IdUser);
            if (!existsUser) return ResultViewModel<string>.Error("Usuário não encontrado");

            var book = await _bookRepository.GetById(request.IdBook);
            if (book is null) return ResultViewModel<string>.Error("Livro não encontrado!");

            var loan = await _repository.GetById(request.IdLoan);
            if (loan is null) return ResultViewModel<string>.Error("Emprestimo não encontrado!");

            if(loan.IdBook != request.IdBook || loan.IdUser != request.IdUser)
                return ResultViewModel<string>.Error("Dados para devolução do emprestimo incorretos!");

            try
            {
                loan.SetReturnDate();
            }
            catch (Exception ex)
            {
                return ResultViewModel<string>.Error("Emprestimo não está ativo!");
            }

            book.SetIncrementQuantity();

            await _bookRepository.Update(book);

            await _repository.Update(loan);

            await _unitOfWork.CommitAsync(); 

            return ResultViewModel<string>.Sucess("Devolução realizada com sucesso!");


        }
    }
}
