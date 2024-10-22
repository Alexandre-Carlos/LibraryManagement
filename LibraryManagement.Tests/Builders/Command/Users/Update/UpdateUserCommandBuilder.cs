using Bogus;
using LibraryManagement.Application.Commands.Users.Update;

namespace LibraryManagement.Tests.Builders.Command.Users.Update
{
    public class UpdateUserCommandBuilder
    {
        private readonly Faker<UpdateUserCommand> instance;

        public UpdateUserCommandBuilder()
        {
            instance = new AutoFaker<UpdateUserCommand>();
        }

        public UpdateUserCommandBuilder WithId(int id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }
        public UpdateUserCommandBuilder WithName(string name)
        {
            instance.RuleFor(x =>x.Name, name);
            return this;
        }
        public UpdateUserCommandBuilder WithEmail(string email)
        {
            instance.RuleFor(x => x.Email, email);
            return this;
        }
        public UpdateUserCommand Build() => instance.Generate();
    }
}
