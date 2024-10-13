using FluentAssertions;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Application.Queries.Users.GetById;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Dtos.Users;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Users.GetById
{
    public class GetUserByIdHandlerTests
    {
        private readonly Mock<IUserRepository> _repository;

        public GetUserByIdHandlerTests()
        {
            _repository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Execute_WhenGetByIdUser_ReturnsSuccess()
        {
            var request = new GetUserByIdQuery(1);

            var user = new UserBuilder().WithId(request.Id).Build();

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(user);

            var userResponseDto = new UserResponseDtoBuilder().Build();

            var responseUserDto = UserResponseDto.FromEntity(user);

            var response = new GetUserByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenGetByIdUserNotFound_ReturnsError()
        {
            var request = new GetUserByIdQuery(1);

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync((User)null);


            var response = new GetUserByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Usuário não encontrado");

            result.Data.Should().BeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
