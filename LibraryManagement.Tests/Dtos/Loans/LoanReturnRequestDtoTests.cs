using FluentAssertions;
using LibraryManagement.Tests.Builders.Dtos.Loans;

namespace LibraryManagement.Tests.Dtos.Loans
{
    public class LoanReturnRequestDtoTests
    {

        [Fact]
        public void Create_LoanReturnDtoIsOk_Success()
        {
            var loanRequestDto = new LoanReturnRequestDtoBuilder().Build();

            loanRequestDto.Should().NotBeNull();
        }
    }
}
