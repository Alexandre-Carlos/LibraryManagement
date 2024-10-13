using FluentAssertions;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Tests.Builders.Dtos.Users;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Dtos.Users
{
    public class UserResponseDtoTest
    {
        [Fact]
        public void Create_UserResponseDtoIsOk_Success()
        {
            var userResponseDto = new UserResponseDtoBuilder().Build();

            userResponseDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_UserFromEntityIsOk_Success()
        {

            var user = new UserBuilder().Build();

            var userResponseDto = UserResponseDto.FromEntity(user);

            userResponseDto.Should().NotBeNull();
            userResponseDto.Id.Should().Be(user.Id);
            userResponseDto.Name.Should().Be(user.Name);
            userResponseDto.Email.Should().Be(user.Email);
        }
    }
}
