using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using MediatR;

namespace LibraryManagement.Application.Queries.Users.GetById
{
    public class GetUserByIdQuery : IRequest<ResultViewModel<UserResponseDto>>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
