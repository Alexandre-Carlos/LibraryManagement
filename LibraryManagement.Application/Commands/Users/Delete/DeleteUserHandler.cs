using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Commands.Users.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResultViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly ILoanRepository _loanRepository;

        public DeleteUserHandler(IUserRepository repository, ILoanRepository loanRepository)
        {
            _repository = repository;
            _loanRepository = loanRepository;
        }

        public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            var loans = await _loanRepository.Exists(request.Id);

            if (loans)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            user.SetAsDeleted();
            await _repository.Update(user);

            return ResultViewModel.Sucess();
        }
    }
}
