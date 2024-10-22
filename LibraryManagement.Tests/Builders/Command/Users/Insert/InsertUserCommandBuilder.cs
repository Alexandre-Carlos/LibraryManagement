using Bogus;
using LibraryManagement.Application.Commands.Users.Insert;

namespace LibraryManagement.Tests.Builders.Command.Users.Insert
{
    public class InsertUserCommandBuilder
    {
        private readonly Faker<InsertUserCommand> instance;

        public InsertUserCommandBuilder()
        {
            instance = new AutoFaker<InsertUserCommand>();
        }

        public InsertUserCommandBuilder WithName(string name)
        {
            instance.RuleFor(x => x.Name, name);
            return this;
        }

        public InsertUserCommandBuilder WithEmail(string email)
        {
            instance.RuleFor(x => x.Email, email);
            return this;
        }

        public InsertUserCommand Build() => instance.Generate();
    }
}
