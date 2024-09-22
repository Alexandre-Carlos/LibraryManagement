using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Books.GetAll
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<BookResponseDto>>>
    {
        private readonly IBookRepository _repository;

        public GetAllBooksHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<BookResponseDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAll();

            var response = books.Select(b => BookResponseDto.FromEntity(b)).ToList();

            return ResultViewModel<List<BookResponseDto>>.Sucess(response);
        }
    }
}
