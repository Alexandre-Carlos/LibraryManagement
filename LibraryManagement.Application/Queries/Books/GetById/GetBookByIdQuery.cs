using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using MediatR;

namespace LibraryManagement.Application.Queries.Books.GetById
{
    public class GetBookByIdQuery : IRequest<ResultViewModel<BookResponseDto>>
    {
        public GetBookByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
