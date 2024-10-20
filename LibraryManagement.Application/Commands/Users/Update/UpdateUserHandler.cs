using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var user = await _repository.GetById(request.Id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            user.Update(request.Name, request.Email);

            await _repository.Update(user);

            await _unitOfWork.CommitAsync();

            return ResultViewModel.Sucess();
        }
    }
}
