using FluentAssertions;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Queries.Loans.GetById;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Dtos.Loans;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Loans.GetById
{
    public class GetLoanByIdHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;

        public GetLoanByIdHandlerTests()
        {
            _repository = new Mock<ILoanRepository>();
        }

        [Fact]
        public async Task Execute_WhenGetByIdLoan_ReturnsSuccess()
        {
            var request = new GetLoanByIdQuery(1);

            var loan = new LoanBuilder().WithId(request.Id).Build();

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(loan);

            var loanResponseDto = new LoanResponseDtoBuilder().Build();

            var responseLoanDto = LoanResponseDto.FromEntity(loan);

            var response = new GetLoanByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenGetByIdLoanNotFound_ReturnsError()
        {
            var request = new GetLoanByIdQuery(1);

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync((Loan)null);


            var response = new GetLoanByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Emprestimo não localizado");

            result.Data.Should().BeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
