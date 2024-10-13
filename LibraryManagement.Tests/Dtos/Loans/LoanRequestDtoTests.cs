using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Dtos.Loans;

namespace LibraryManagement.Tests.Dtos.Loans
{
    public class LoanRequestDtoTests
    {
        [Fact]
        public void Create_LoanRequestDtoIsOk_Success()
        {
            var loanRequestDto = new LoanRequestDtoBuilder().Build();

            loanRequestDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_LoanEntityIsOk_Success()
        {
            var returnDays = 30;
            var dateOfLoan = DateTime.Now.Date;
            var loanRequestDto = new LoanRequestDtoBuilder().Build();

            var loan = loanRequestDto.ToEntity(returnDays);

            loan.Should().NotBeNull();
            loan.Should().GetType().Equals(typeof(Loan));


            loan.IdBook.Should().Be(loanRequestDto.IdBook);
            loan.IdUser.Should().Be(loanRequestDto.IdUser);

            loan.Active.Should().BeTrue();
            loan.DateOfLoan.Date.Should().Be(dateOfLoan);
            loan.EndDateLoan.Date.Should().Be(dateOfLoan.AddDays(returnDays));

        }
    }
}
