using Bogus;
using LibraryManagement.Application.Dtos.Loans;

namespace LibraryManagement.Tests.Builders.Dtos.Loans
{
    public class LoanResponseDtoBuilder
    {
        private readonly Faker<LoanResponseDto> instance;

        public LoanResponseDtoBuilder()
        {
            instance = new AutoFaker<LoanResponseDto>();
        }

        public LoanResponseDtoBuilder WithId(int id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }
        public LoanResponseDtoBuilder WithIdUser(int idUser)
        {
            instance.RuleFor(x => x.IdUser, idUser); 
            return this;
        }
        
        public LoanResponseDtoBuilder WithFullNameUser(string fullNameUser)
        {
            instance.RuleFor(x => x.FullNameUser, fullNameUser);
            return this;
        }

        public LoanResponseDtoBuilder WithIdBook(int idBook)
        {
            instance.RuleFor(x => x.IdBook, idBook);
            return this;
        }
        
        public LoanResponseDtoBuilder WithTitleBook(string titleBook)
        {
            instance.RuleFor(x => x.TitleBook, titleBook);
            return this;
        }
    
        public LoanResponseDtoBuilder WithDateOfLoan(DateTime dateOfLoan)
        {
            instance.RuleFor(x => x.DateOfLoan, dateOfLoan);
            return this;
        }
        public LoanResponseDtoBuilder WithEndDateLoan(DateTime endDateOfLoan)
        {
            instance.RuleFor(x => x.EndDateLoan, endDateOfLoan);
            return this;
        }
        public LoanResponseDto Build() => instance.Generate();
    }
}
