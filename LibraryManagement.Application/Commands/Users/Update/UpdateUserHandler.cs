using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            user.Update(request.Name, request.Email);

            await _repository.Update(user);

            return ResultViewModel.Sucess();
        }
    }
}
