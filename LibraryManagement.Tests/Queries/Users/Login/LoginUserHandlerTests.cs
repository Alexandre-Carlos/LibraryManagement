using FluentAssertions;
using LibraryManagement.Application.Queries.Users.Login;
using LibraryManagement.Core.Account;
using LibraryManagement.Tests.Builders.Queries.User;
using Moq;

namespace LibraryManagement.Tests.Queries.Users.Login
{
    public class LoginUserHandlerTests
    {
        private readonly Mock<IAuthenticate> _authenticate;

        public LoginUserHandlerTests()
        {
            _authenticate = new Mock<IAuthenticate>();
        }

        [Fact]
        public async Task Execute_WhenToAuthenticateUser_ReturnsSuccess()
        {
            var request = new LoginUserQueryBuilder().Build();
            var token = "$HASH|V1$10000$s40yWJytZ6qfP+jXX9Kv4iHqZO6OumAi43n8XbPuS3F2OIwq";

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(true);

            _authenticate.Setup(u => u.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(token);

            var response = new LoginUserHandler(_authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNullOrEmpty();

            _authenticate.Verify(r => r.UserExist(It.IsAny<string>()), Times.Once);
            _authenticate.Verify(r => r.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToAuthenticateUser_Error()
        {
            var request = new LoginUserQueryBuilder().Build();
            var token = "$HASH|V1$10000$s40yWJytZ6qfP+jXX9Kv4iHqZO6OumAi43n8XbPuS3F2OIwq";

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(true);

            _authenticate.Setup(u => u.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");

            var response = new LoginUserHandler(_authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.Data.Should().BeNullOrEmpty();
            result.Message.Should().BeSameAs("Erro no Login!");

            _authenticate.Verify(r => r.UserExist(It.IsAny<string>()), Times.Once);
            _authenticate.Verify(r => r.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToAuthenticateUser_EmailError()
        {
            var request = new LoginUserQueryBuilder().Build();
            var token = "$HASH|V1$10000$s40yWJytZ6qfP+jXX9Kv4iHqZO6OumAi43n8XbPuS3F2OIwq";

            _authenticate.Setup(u => u.UserExist(It.IsAny<string>())).ReturnsAsync(false);

            var response = new LoginUserHandler(_authenticate.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.Data.Should().BeNullOrEmpty();
            result.Message.Should().BeSameAs("Erro no Login!");

            _authenticate.Verify(r => r.UserExist(It.IsAny<string>()), Times.Once);
        }
    }
}
