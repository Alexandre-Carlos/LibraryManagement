using Bogus;
using LibraryManagement.Application.Commands.Loans.ReturnLoan;

namespace LibraryManagement.Tests.Builders.Command.Loans.ReturnLoan
{
    public class ReturnLoanCommandBuilder
    {
        private readonly Faker<ReturnLoanCommand> instance;

        public ReturnLoanCommandBuilder()
        {
            instance = new AutoFaker<ReturnLoanCommand>();
        }

        public ReturnLoanCommandBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(x => x.IdUser, idUser);
            return this;
        }
        public ReturnLoanCommandBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(x => x.IdBook, idBook);
            return this;
        }

        public ReturnLoanCommandBuilder WithIdLoan(int idLoan)
        {
            instance.RuleFor(x => x.IdLoan, idLoan);
            return this;
        }

        public ReturnLoanCommand Build() => instance.Generate();
    }
}
