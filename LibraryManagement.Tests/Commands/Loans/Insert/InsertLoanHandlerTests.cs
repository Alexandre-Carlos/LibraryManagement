using FluentAssertions;
using LibraryManagement.Api.Configuration;
using LibraryManagement.Application.Commands.Loans.Insert;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Tests.Builders.Command.Loans.Insert;
using LibraryManagement.Tests.Builders.Entities;
using Microsoft.Extensions.Options;
using Moq;

namespace LibraryManagement.Tests.Commands.Loans.Insert
{
    public class InsertLoanHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly int _returnDays = 30;
        private readonly IOptions<ReturnDaysConfig> _options;

        private readonly IOptions<ReturnDaysConfig> options = Options.Create<ReturnDaysConfig>(new ReturnDaysConfig());

        public InsertLoanHandlerTests()
        {
            _bookRepository = new Mock<IBookRepository>();
            _repository = new Mock<ILoanRepository>();
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _options = options;
        }

        [Fact]
        public async Task Execute_WhenToInserLoan_ReturnsSuccess()
        {
            var request = new InsertLoanCommandBuilder().Build();

            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            var book = new BookBuilder().WithId(request.IdBook).Build();
            _bookRepository.Setup(b => b.GetByIdAndHasQuantity(It.IsAny<int>())).ReturnsAsync(book);

            var user = new UserBuilder().WithId(request.IdUser).Build();
            _userRepository.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);

            book.Invoking(b => b.SetDecrementQuantity());

            request.Invoking(c => c.ToEntity(_returnDays));

            var resposta = request.ToEntity(_returnDays);

            var loan = new LoanBuilder()
           .WithIdUser(request.IdUser)
           .WithIdBook(request.IdBook)
           .WithBook(book)
           .WithUser(user)
           .Build();

            _bookRepository.Setup(b => b.Update(It.IsAny<Book>()));

            _repository.Setup(l => l.Add(It.IsAny<Loan>())).ReturnsAsync(loan.Id);

            var loanResponse = LoanResponseDto.FromEntity(loan);
       

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new InsertLoanHandler(_repository.Object,_bookRepository.Object,_userRepository.Object, _options, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);

            _userRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetByIdAndHasQuantity(It.IsAny<int>()), Times.Once);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);

            _repository.Verify(l => l.Add(It.IsAny<Loan>()), Times.Once);


        }
    }
}
