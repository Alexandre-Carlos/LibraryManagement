using Bogus;
using LibraryManagement.Application.Commands.Loans.Insert;

namespace LibraryManagement.Tests.Builders.Command.Loans.Insert
{
    public class InsertLoanCommandBuilder
    {
        private readonly Faker<InsertLoanCommand> instance;

        public InsertLoanCommandBuilder()
        {
            instance = new AutoFaker<InsertLoanCommand>();
        }

        public InsertLoanCommandBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(x => x.IdUser, idUser);
            return this;
        }
        public InsertLoanCommandBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(x => x.IdBook, idBook);
            return this;
        }

        public InsertLoanCommand Build() => instance.Generate();
    }
}
