using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Dtos.Users;

namespace LibraryManagement.Tests.Dtos.Users
{
    public class UserRequestDtoTests
    {
        [Fact]
        public void Create_UserRequestDtoIsOk_Success()
        {
            var userRequestDto = new UserRequestDtoBuilder().Build();

            userRequestDto.Should().NotBeNull();

        }

        [Fact]
        public void Returns_UserEntityIsOk_Success()
        {
            var userRequestDto = new UserRequestDtoBuilder().Build();

            var user = userRequestDto.ToEntity();

            user.Should().GetType().Equals(typeof(User));

            user.Name.Should().Be(userRequestDto.Name);
            user.Email.Should().Be(userRequestDto.Email);
        }
    }
}
