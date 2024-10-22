using FluentAssertions;
using LibraryManagement.Application.Commands.Users.Update;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Tests.Builders.Command.Users.Update;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Commands.Users.Update
{
    public class UpdateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateUserHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Execute_WhenToUpdateUser_ReturnsSuccess()
        {
            var request = new UpdateUserCommandBuilder().Build();

            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var user = new UserBuilder().Build();

            _userRepository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);

            user.Update(request.Name, request.Email);

            _userRepository.Setup(u => u.Update(It.IsAny<User>()));

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new UpdateUserHandler(_userRepository.Object, _unitOfWork.Object);

            var result =await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _userRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _userRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);

        }

        [Fact]
        public async Task Execute_WhenToUpdateUser_ReturnsError()
        {
            var request = new UpdateUserCommandBuilder().Build();

            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var user = new UserBuilder().Build();

            _userRepository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync((User?)null);

        
            var response = new UpdateUserHandler(_userRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
