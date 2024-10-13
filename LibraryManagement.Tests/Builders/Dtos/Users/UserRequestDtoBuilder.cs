using Bogus;
using LibraryManagement.Application.Dtos.Users;

namespace LibraryManagement.Tests.Builders.Dtos.Users
{
    public class UserRequestDtoBuilder
    {
        private readonly Faker<UserRequestDto> instance;

        public UserRequestDtoBuilder()
        {
            instance = new AutoFaker<UserRequestDto>();
        }

        public UserRequestDtoBuilder WithName(string name)
        {
            instance.RuleFor(u => u.Name, name);
            return this;
        }

        public UserRequestDtoBuilder WithEmail(string email)
        {
            instance.RuleFor(u => u.Email, email);
            return this;
        }

        public UserRequestDto Build() => instance.Generate();
    }
}
