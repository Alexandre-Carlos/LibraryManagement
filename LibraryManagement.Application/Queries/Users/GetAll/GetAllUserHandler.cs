using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Users.GetAll
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, ResultViewModel<List<UserResponseDto>>>
    {
        private readonly IUserRepository _repository;

        public GetAllUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<UserResponseDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAll();

            var response = user.Select(u => UserResponseDto.FromEntity(u)).ToList();

            return ResultViewModel<List<UserResponseDto>>.Sucess(response);
        }
    }
}
