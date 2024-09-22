using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Insert
{
    public class InsertUserCommand : IRequest<ResultViewModel<int>>
    {
        public string Name { get; set; }
        public string Email { get; set; }


        public User ToEntity() => new(Name, Email);
    }
}
