using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Insert
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _repository;

        public InsertUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _repository.Add(user);

            var response = UserResponseDto.FromEntity(user);

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
