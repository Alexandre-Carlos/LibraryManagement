using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Delete
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, ResultViewModel>
    {
        private readonly IBookRepository _repository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookHandler(IBookRepository repository, ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var book = await _repository.GetById(request.Id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            var loan = await _loanRepository.Exists(request.Id);

            if (loan)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            book.SetAsDeleted();

            await _repository.Update(book);

            await _unitOfWork.CommitAsync();
            
            return ResultViewModel.Sucess();
        }
    }
}
