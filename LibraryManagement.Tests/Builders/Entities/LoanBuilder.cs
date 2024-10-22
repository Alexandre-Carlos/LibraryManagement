using Bogus;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Tests.Builders.Entities
{
    public class LoanBuilder
    {
        public readonly Faker<Loan> instance;

        public LoanBuilder()
        {
            instance = new AutoFaker<Loan>()
                .RuleFor(b => b.User, Faker =>
                    new UserBuilder().Build()
                )
                .RuleFor(u => u.Book, Faker =>
                    new BookBuilder().Build()
            );
        }

        public LoanBuilder WithId(int id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }

        public LoanBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(x => x.IdUser, idUser);
            return this;
        }

        public LoanBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(x => x.IdBook, idBook);
            return this;
        }

        public LoanBuilder WithDateOfLoan(DateTime dateOfLoan) 
        {
            instance.RuleFor(l => l.DateOfLoan, dateOfLoan);
            return this;
        }
        
        public LoanBuilder WithEndDateLoan(DateTime endDateLoan)
        {
            instance.RuleFor(l => l.EndDateLoan, endDateLoan);
            return this;
        }

        public LoanBuilder WithReturnDate(DateTime returnDate)
        {
            instance.RuleFor(l => l.ReturnDate, returnDate);
            return this;
        }

        public LoanBuilder WithDaysOfDelay(int daysOfDelay)
        {
            instance.RuleFor(l => l.DaysOfDelay, daysOfDelay);
            return this;
        }

        public LoanBuilder WithActive(bool active)
        { 
            instance.RuleFor(l => l.Active, active);
            return this;
        }

        public LoanBuilder WithUser(User user)
        {
            instance.RuleFor(l => l.User, user);
            return this;
        }

        public LoanBuilder WithBook(Book book)
        {
            instance.RuleFor(l => l.Book, book);
            return this;
        }

        public Loan Build() => instance.Generate();
    }
}
