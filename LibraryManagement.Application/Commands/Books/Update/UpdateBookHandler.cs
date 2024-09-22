using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Update
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, ResultViewModel>
    {
        private readonly IBookRepository _repository;

        public UpdateBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.Id);
            if (book is null) return ResultViewModel.Error("Livro não encontrado!");

            book.Update(request.Title, request.Author, request.Isbn, request.YearPublished);

            await _repository.Update(book);

            return ResultViewModel.Sucess();
        }
    }
}
