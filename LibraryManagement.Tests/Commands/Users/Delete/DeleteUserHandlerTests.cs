using FluentAssertions;
using LibraryManagement.Application.Commands.Users.Delete;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Commands.Users.Delete
{
    public class DeleteUserHandlerTests
    {
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ILoanRepository> _loanRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeleteUserHandlerTests()
        {
            _repository = new Mock<IUserRepository>();
            _loanRepository = new Mock<ILoanRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Execute_WhenToDeleteUser_ReturnsSuccess()
        {
            var request = new DeleteUserCommand(1);

            var user = new UserBuilder().WithId(request.Id).Build();

            var loan = new LoanBuilder().WithIdUser(2).WithActive(false).Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _repository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);

            _loanRepository.Setup(l => l.ExistsUser(It.IsAny<int>())).ReturnsAsync(false);

            user.Invoking(u => u.SetAsDeleted());

            _repository.Setup(u => u.Update(It.IsAny<User>()));

            _unitOfWork.Setup(uow => uow.CommitAsync());

            var response = new DeleteUserHandler(_repository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);

            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);

            _loanRepository.Verify(r => r.ExistsUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToDeleteUserButLoanActive_ReturnsError()
        {
            var request = new DeleteUserCommand(1);

            var user = new UserBuilder().WithId(request.Id).Build();

            var loan = new LoanBuilder().WithIdUser(request.Id).WithActive(false).Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _repository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);

            _loanRepository.Setup(l => l.ExistsUser(It.IsAny<int>())).ReturnsAsync(true);

            var response = new DeleteUserHandler(_repository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);

            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _repository.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);

            _loanRepository.Verify(r => r.ExistsUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToDeleteUserNotFound_ReturnsError()
        {
            var request = new DeleteUserCommand(1);


            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _repository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync((User)null);

     
            var response = new DeleteUserHandler(_repository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Usuário não encontrado");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);

            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _repository.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);

            _loanRepository.Verify(r => r.ExistsUser(It.IsAny<int>()), Times.Never);
        }
    }
}
