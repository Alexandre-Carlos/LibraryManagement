using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Update
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookHandler(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.Id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            book.Update(request.Title, request.Author, request.Isbn, request.YearPublished);

            await _unitOfWork.BeginTransactionAsync();

            await _repository.Update(book);                      

            await _unitOfWork.CommitAsync();

            return ResultViewModel.Sucess();
        }
    }
}
