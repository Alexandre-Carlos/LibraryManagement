using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Services.Authorize;
using LibraryManagement.Core.Account;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Insert
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthenticate _authenticate;
        private readonly IUnitOfWork _unitOfWork;

        public InsertUserHandler(IUserRepository repository, IUnitOfWork unitOfWork, IAuthenticate authenticate)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _authenticate = authenticate;
        }

        public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var user = request.ToEntity();

            var isUserExist = await _authenticate.UserExist(request.Email);
            if (!isUserExist) return ResultViewModel.Error("Erro no Login!");

            var hashResponse = await _authenticate.GenerateHashPassword(request.Email, request.Password);

            if(hashResponse == null) return ResultViewModel.Error("Erro no Login!");

            user.SetHashPassword(hashResponse.Hash, hashResponse.Salt);

            await _repository.Add(user);

            var response = UserResponseDto.FromEntity(user);

            await _unitOfWork.CommitAsync();

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
