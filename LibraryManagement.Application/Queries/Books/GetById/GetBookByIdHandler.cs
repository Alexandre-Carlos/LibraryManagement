using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Books.GetById
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, ResultViewModel<BookResponseDto>>
    {
        private readonly IBookRepository _repository;

        public GetBookByIdHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<BookResponseDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.Id);

            if (book is null) return ResultViewModel<BookResponseDto>.Error("Livro não encontrado");

            var response = BookResponseDto.FromEntity(book);

            return ResultViewModel<BookResponseDto>.Sucess(response);
        }
    }
}
