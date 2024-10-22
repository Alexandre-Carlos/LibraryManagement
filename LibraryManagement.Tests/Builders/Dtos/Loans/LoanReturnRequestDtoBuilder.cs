using Bogus;
using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Tests.Builders.Dtos.Loans
{
    public class LoanReturnRequestDtoBuilder
    {
        private readonly Faker<LoanReturnRequestDto> instance;

        public LoanReturnRequestDtoBuilder()
        {
            instance = new AutoFaker<LoanReturnRequestDto>();
        }
        public LoanReturnRequestDtoBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(x => x.IdUser, idUser);
            return this;
        }

        public LoanReturnRequestDtoBuilder WithFullNameUser(int idLoan)
        {
            instance.RuleFor(x => x.IdLoan, idLoan);
            return this;
        }

        public LoanReturnRequestDtoBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(x => x.IdBook, idBook);
            return this;
        }

        public LoanReturnRequestDto Build() => instance.Generate();
    }
}
