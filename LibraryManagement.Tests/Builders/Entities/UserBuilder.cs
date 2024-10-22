using Bogus;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Tests.Builders.Entities
{
    public class UserBuilder
    {
        public readonly Faker<User> instance;

        public UserBuilder()
        {
            instance = new AutoFaker<User>();
           
        }

        public UserBuilder WithId(int id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }
        public UserBuilder WithName(string name) 
        {
            instance.RuleFor(u => u.Name, name);
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            instance.RuleFor(u => u.Email, email);
            return this;
        }

        public UserBuilder WithLoans(List<Loan> loan)
        { 
            instance.RuleFor(l => l.Loans, loan);
            return this;
        }

        public User Build() => instance.Generate();

    }
}
