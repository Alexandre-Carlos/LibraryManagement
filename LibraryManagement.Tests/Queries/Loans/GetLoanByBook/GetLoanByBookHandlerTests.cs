using FluentAssertions;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Queries.Loans.GetLoanByBook;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Loans.GetLoanByBook
{
    public class GetLoanByBookHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;

        public GetLoanByBookHandlerTests()
        {
            _repository = new Mock<ILoanRepository>();
        }

        [Fact]
        public async Task Execute_WhenToGetAllBookLoan_ReturnsSuccess()
        {
            var request = new GetLoanByBookQuery(1,1);
            var loans = new List<Loan> { new LoanBuilder().Build(), new LoanBuilder().Build() };

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAllBookByUserLoan(request.UserId, request.BookId)).ReturnsAsync(loans);

            var response = new GetLoanByBookHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNullOrEmpty();

            _repository.Verify(b => b.GetAllBookByUserLoan(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToGetAllBookLoanEmpty_ReturnsSuccess()
        {
            var request = new GetLoanByBookQuery(1,1);
            var loans = new List<Loan>();

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAllBookByUserLoan(request.UserId, request.BookId)).ReturnsAsync(loans);

            var response = new GetLoanByBookHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().BeEmpty();

            _repository.Verify(b => b.GetAllBookByUserLoan(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
