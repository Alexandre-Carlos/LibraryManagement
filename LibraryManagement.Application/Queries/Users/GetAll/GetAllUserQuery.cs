using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using MediatR;

namespace LibraryManagement.Application.Queries.Users.GetAll
{
    public class GetAllUserQuery : IRequest<ResultViewModel<List<UserResponseDto>>>
    {
    }
}
