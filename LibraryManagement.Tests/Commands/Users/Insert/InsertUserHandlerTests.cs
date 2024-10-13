using FluentAssertions;
using LibraryManagement.Application.Commands.Users.Insert;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Tests.Builders.Command.Users.Insert;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Commands.Users.Insert
{
    public class InsertUserHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IUserRepository> _userRepository;

        public InsertUserHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Execute_WhenToInsertUser_ReturnsSuccess()
        {
            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var request = new InsertUserCommandBuilder().Build();

            var userRequest = request.ToEntity();

            var user = new UserBuilder().WithName(request.Name).WithEmail(request.Email).Build();

            _userRepository.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(user.Id);

            var userResponse = UserResponseDto.FromEntity(user);

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new InsertUserHandler(_userRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _userRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        } 
    }
}
