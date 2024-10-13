using FluentAssertions;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Queries.Loans.GetAll;
using LibraryManagement.Application.Queries.Loans.GetAllUserLoan;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Loans.GetAllUserLoan
{
    public class GetAllUserLoanHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;

        public GetAllUserLoanHandlerTests()
        {
            _repository = new Mock<ILoanRepository>();
        }

        [Fact]
        public async Task Execute_WhenToGetAllUserLoan_ReturnsSuccess()
        {
            var request = new GetAllUserLoanQuery(1);
            var loans = new List<Loan> { new LoanBuilder().Build(), new LoanBuilder().Build() };

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAllUserLoan(request.IdUser)).ReturnsAsync(loans);

            var response = new GetAllUserLoanHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNullOrEmpty();

            _repository.Verify(b => b.GetAllUserLoan(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToGetAllUserLoanEmpty_ReturnsSuccess()
        {
            var request = new GetAllUserLoanQuery(1);
            var loans = new List<Loan>();

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAllUserLoan(request.IdUser)).ReturnsAsync(loans);

            var response = new GetAllUserLoanHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().BeEmpty();

            _repository.Verify(b => b.GetAllUserLoan(It.IsAny<int>()), Times.Once);
        }
    }
}
