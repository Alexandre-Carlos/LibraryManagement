using FluentAssertions;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Tests.Builders.Dtos.Loans;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Dtos.Loans
{
    public class LoanResponseDtoTests
    {

        [Fact]
        public void Create_LoanResponseDtoIsOk_Success()
        {
            var loanResponseDto = new LoanResponseDtoBuilder().Build();

            loanResponseDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_LoanResponseFromEntityIsOk_Success()
        {
            var returnDays = 30;
            var dateOfLoan = DateTime.Now.Date;
            var loan = new LoanBuilder()
                .WithDateOfLoan(DateTime.Now)
                .WithEndDateLoan(DateTime.Now.AddDays(returnDays))
                .Build();

            var loanResponseDto = LoanResponseDto.FromEntity(loan);


            loanResponseDto.Should().NotBeNull();
            loanResponseDto.Should().GetType().Equals(typeof(LoanRequestDto));


            loanResponseDto.IdBook.Should().Be(loanResponseDto.IdBook);
            loanResponseDto.IdUser.Should().Be(loanResponseDto.IdUser);
            loanResponseDto.DateOfLoan.Date.Should().Be(dateOfLoan);
            loanResponseDto.EndDateLoan.Date.Should().Be(dateOfLoan.AddDays(returnDays));

        }
    }
}
