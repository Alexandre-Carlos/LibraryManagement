using LibraryManagement.Application.Dtos;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Delete
{
    public class DeleteBookCommand : IRequest<ResultViewModel>
    {
        public DeleteBookCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
