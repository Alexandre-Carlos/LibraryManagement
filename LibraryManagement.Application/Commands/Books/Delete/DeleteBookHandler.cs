using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Delete
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, ResultViewModel>
    {
        private readonly IBookRepository _repository;
        private readonly ILoanRepository _loanRepository;

        public DeleteBookHandler(IBookRepository repository, ILoanRepository loanRepository)
        {
            _repository = repository;
            _loanRepository = loanRepository;
        }

        public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.Id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            var loan = await _loanRepository.Exists(request.Id);

            if (loan)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            book.SetAsDeleted();

            await _repository.Update(book);
            
            return ResultViewModel.Sucess();
        }
    }
}
