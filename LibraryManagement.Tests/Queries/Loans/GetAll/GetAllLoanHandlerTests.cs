using FluentAssertions;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Queries.Loans.GetAll;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Loans.GetAll
{
    public class GetAllLoanHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;

        public GetAllLoanHandlerTests()
        {
            _repository = new Mock<ILoanRepository>();
        }

        [Fact]
        public async Task Execute_WhenToGetAllLoan_ReturnsSuccess()
        {
            var request = new GetAllLoanQuery();
            var loans = new List<Loan> { new LoanBuilder().Build(), new LoanBuilder().Build() };

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAll()).ReturnsAsync(loans);

            var response = new GetAllLoanHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNullOrEmpty();

            _repository.Verify(b => b.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToGetAllLoanEmpty_ReturnsSuccess()
        {
            var request = new GetAllLoanQuery();
            var loans = new List<Loan>();

            var responseLoanDto = loans.Select(b => LoanResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAll()).ReturnsAsync(loans);

            var response = new GetAllLoanHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().BeEmpty();

            _repository.Verify(b => b.GetAll(), Times.Once);
        }
    }
}
