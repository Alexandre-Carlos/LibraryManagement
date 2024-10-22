using Bogus;
using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Tests.Builders.Dtos.Loans
{
    public class LoanRequestDtoBuilder
    {
        private readonly Faker<LoanRequestDto> instance;

        public LoanRequestDtoBuilder()
        {
            instance = new AutoFaker<LoanRequestDto>();
        }

        public LoanRequestDtoBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(l => l.IdUser, idUser);
            return this;
        }

        public LoanRequestDtoBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(l => l.IdBook, idBook);
            return this;
        }


        public LoanRequestDto Build() => instance.Generate();
    }
}
