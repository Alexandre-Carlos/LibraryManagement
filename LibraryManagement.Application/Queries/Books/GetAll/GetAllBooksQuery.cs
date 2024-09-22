using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using MediatR;

namespace LibraryManagement.Application.Queries.Books.GetAll
{
    public class GetAllBooksQuery : IRequest<ResultViewModel<List<BookResponseDto>>>
    {

    }
}
