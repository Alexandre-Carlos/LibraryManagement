using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Insert
{
    public class InsertBookHandler : IRequestHandler<InsertBookCommand, ResultViewModel<int>>
    {
        private readonly IBookRepository _repository;

        public InsertBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertBookCommand request, CancellationToken cancellationToken)
        {
            var book = request.ToEntity();

            await _repository.Add(book);

            var response = BookResponseDto.FromEntity(book);

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
