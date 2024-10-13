using FluentAssertions;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Queries.Users.GetAll;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Users.GetAll
{
    public class GetAllUserHandlerTests
    {
        private readonly Mock<IUserRepository> _repository;

        public GetAllUserHandlerTests()
        {
            _repository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Execute_WhenToGetAllUser_ReturnsSuccess()
        {
            var request = new GetAllUserQuery();
            var users = new List<User> { new UserBuilder().Build(), new UserBuilder().Build() };

            var responseUserDto = users.Select(b => UserResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAll()).ReturnsAsync(users);

            var response = new GetAllUserHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Execute_WhenToGetAllUserEmpty_ReturnsSuccess()
        {
            var request = new GetAllUserQuery();
            var users = new List<User>();

            var responseUserDto = users.Select(b => UserResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAll()).ReturnsAsync(users);

            var response = new GetAllUserHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().BeEmpty();
        }
    }
}
