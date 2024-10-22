using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
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

            var book = await _repository.GetById(request.Id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            var loan = await _loanRepository.ExistsBook(request.Id);

            if (loan)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");


            await _unitOfWork.BeginTransactionAsync();

            book.SetAsDeleted();

            await _repository.Update(book);

            await _unitOfWork.CommitAsync();
            
            return ResultViewModel.Sucess();
        }
    }
}
