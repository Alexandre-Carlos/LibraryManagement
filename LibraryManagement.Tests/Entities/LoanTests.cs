using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Entities
{
    public class LoanTests
    {
        [Fact]
        public void Create_LoanIsOk_Success()
        {
            var loan = new LoanBuilder().Build(); 

            loan.Should().NotBeNull();
        }

        [Fact]
        public void Create_LoanConstrutorDataIsOk_Success()
        {
            var loan = new Loan(1,1,30);

            loan.Should().NotBeNull();
        }

        [Fact]
        public void Check_LoanIsActive_Success()
        {
            var loan = new LoanBuilder().WithActive(true) .Build();

            loan.Active.Should().BeTrue();

        }

        [Fact]
        public void Check_LoanReturnDateFilled_Success()
        {
            var loan = new LoanBuilder().WithActive(true).WithReturnDate(DateTime.Now).Build();

            loan.SetReturnDate();

            loan.ReturnDate.Date.Should().Be(DateTime.Now.Date);

        }

        [Fact]
        public void Check_UserIsDeletecOK_Success()
        {
            var loan = new LoanBuilder().Build();

            loan.SetAsDeleted();

            loan.IsDeleted.Should().BeTrue();
        }


        [Fact]
        public void Update_LoanActiveIsNotOk_ThrowExeception()
        {
            var loan = new LoanBuilder().WithActive(true).Build();

            loan.SetReturnDate();

            loan.Invoking(l => l.SetReturnDate())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Emprestimo não está ativo!");
        }

    }
}
