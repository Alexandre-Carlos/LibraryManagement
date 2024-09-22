using LibraryManagement.Application.Dtos;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Delete
{
    public class DeleteUserCommand : IRequest<ResultViewModel>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
