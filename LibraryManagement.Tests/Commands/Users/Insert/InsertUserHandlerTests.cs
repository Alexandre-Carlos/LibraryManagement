using FluentAssertions;
using LibraryManagement.Application.Commands.Users.Insert;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Services.Authorize;
using LibraryManagement.Core.Account;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Command.Users.Insert;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Commands.Users.Insert
{
    public class InsertUserHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IAuthenticate> _authenticate;

        public InsertUserHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _authenticate = new Mock<IAuthenticate>();
        }

        [Fact]
        public async Task Execute_WhenToInsertUser_ReturnsSuccess()
        {
            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var request = new InsertUserCommandBuilder().Build();

            var userRequest = request.ToEntity();

            var user = new UserBuilder().WithName(request.Name).WithEmail(request.Email).Build();

            _userRepository.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(user.Id);

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(true);

            var hashResponse = new HashResponse() { Hash = "$HASH|V1$10000$s40yWJytZ6qfP+jXX9Kv4iHqZO6OumAi43n8XbPuS3F2OIwq", Salt = "s40yWJytZ6qfP+jXX9Kv4g==" };

            _authenticate.Setup(u => u.GenerateHashPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(hashResponse);

            user.Invoking(x => x.SetHashPassword(hashResponse.Hash, hashResponse.Salt));

            var userResponse = UserResponseDto.FromEntity(user);

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new InsertUserHandler(_userRepository.Object, _unitOfWork.Object, _authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _userRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToInsertUser_ReturnsEmailError()
        {
            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var request = new InsertUserCommandBuilder().Build();

            var userRequest = request.ToEntity();

            var user = new UserBuilder().WithName(request.Name).WithEmail(request.Email).Build();

            _userRepository.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(user.Id);

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(false);

            var response = new InsertUserHandler(_userRepository.Object, _unitOfWork.Object, _authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.Message.Should().BeSameAs("Erro no Login!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);

            _authenticate.Verify(r => r.UserExist(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToInsertUser_ReturnsError()
        {
            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var request = new InsertUserCommandBuilder().Build();

            var userRequest = request.ToEntity();

            var user = new UserBuilder().WithName(request.Name).WithEmail(request.Email).Build();

            _userRepository.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(user.Id);

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(true);

            _authenticate.Setup(u => u.GenerateHashPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((HashResponse)null);

            var response = new InsertUserHandler(_userRepository.Object, _unitOfWork.Object, _authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

             result.Message.Should().BeSameAs("Erro no Login!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);

            _authenticate.Verify(r => r.UserExist(It.IsAny<string>()), Times.Once);

            _authenticate.Verify(r => r.GenerateHashPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
