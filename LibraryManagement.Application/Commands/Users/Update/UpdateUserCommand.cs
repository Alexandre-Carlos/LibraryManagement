using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Update
{
    public class UpdateUserCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
