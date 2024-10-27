using Bogus;
using LibraryManagement.Application.Queries.Users.Login;

namespace LibraryManagement.Tests.Builders.Queries.User
{
    public class LoginUserQueryBuilder
    {
        private readonly Faker<LoginUserQuery> instance;

        public LoginUserQueryBuilder()
        {
            instance = new AutoFaker<LoginUserQuery>();
        }

        public LoginUserQuery Build() => instance.Generate();
    }
}
