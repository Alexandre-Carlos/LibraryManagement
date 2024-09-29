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


        public Loan Build() => instance.Generate();
    }
}
