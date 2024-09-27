using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUserRepository repository, ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            var user = await _repository.GetById(request.Id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            var loans = await _loanRepository.Exists(request.Id);

            if (loans)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            user.SetAsDeleted();
            await _repository.Update(user);

            await _unitOfWork.CommitAsync();

            return ResultViewModel.Sucess();
        }
    }
}
