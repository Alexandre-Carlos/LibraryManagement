using Bogus;
using LibraryManagement.Application.Dtos.Users;

namespace LibraryManagement.Tests.Builders.Dtos.Users
{
    public class UserResponseDtoBuilder
    {
        private readonly Faker<UserResponseDto> instance;

        public UserResponseDtoBuilder()
        {
            instance = new AutoFaker<UserResponseDto>();
        }

        public UserResponseDtoBuilder WithId(int id)
        {
            instance.RuleFor(u => u.Id, id);
            return this;
        }

        public UserResponseDtoBuilder WithName(string name)
        {
            instance.RuleFor(u => u.Name, name);
            return this;
        }

        public UserResponseDtoBuilder WithEmail(string email)
        {
            instance.RuleFor(u => u.Email, email);
            return this;
        }

        public UserResponseDto Build() => instance.Generate();
    }
}
