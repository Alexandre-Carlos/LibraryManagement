using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Insert
{
    public class InsertUserHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public InsertUserHandler(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            var user = request.ToEntity();

            await _repository.Add(user);

            var response = UserResponseDto.FromEntity(user);

            await _unitOfWork.CommitAsync();

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
