using FluentAssertions;
using LibraryManagement.Application.Commands.Loans.ReturnLoan;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Tests.Builders.Command.Loans.ReturnLoan;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Commands.Loans.ReturnLoan
{
    public class ReturnLoanHandlerTests
    {
        private readonly Mock<ILoanRepository> _repository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public ReturnLoanHandlerTests()
        {
            _repository = new Mock<ILoanRepository>();
            _userRepository = new Mock<IUserRepository>();
            _bookRepository = new Mock<IBookRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Execute_WhenToReturnLoan_ReturnsSuccess()
        {
            var request = new ReturnLoanCommandBuilder().Build();
            var book = new BookBuilder().WithId(request.IdBook).Build();
            var loan = new LoanBuilder()
                        .WithId(request.IdLoan)
                        .WithIdBook(request.IdBook)
                        .WithIdUser(request.IdUser)
                        .WithActive(true)
                        .Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(true);

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _repository.Setup(l => l.GetById(It.IsAny<int>())).ReturnsAsync(loan);

            loan.Invoking(l => l.SetReturnDate());
            book.Invoking(b => b.SetIncrementQuantity());

            _bookRepository.Setup(b => b.Update(It.IsAny<Book>()));

            _unitOfWork.Setup(uow => uow.CommitAsync());

            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().Be("Devolução realizada com sucesso!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);


        }

        [Fact]
        public async Task Execute_WhenToReturnLoanNotActive_ReturnsError()
        {
            var request = new ReturnLoanCommandBuilder().Build();
            var book = new BookBuilder().WithId(request.IdBook).Build();
            var loan = new LoanBuilder()
                        .WithId(request.IdLoan)
                        .WithIdBook(request.IdBook)
                        .WithIdUser(request.IdUser)
                        .WithActive(false)
                        .Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(true);

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _repository.Setup(l => l.GetById(It.IsAny<int>())).ReturnsAsync(loan);

            loan.Invoking(l => l.SetReturnDate()).Should().ThrowExactly<InvalidOperationException>()
                    .WithMessage("Emprestimo não está ativo!");


            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Emprestimo não está ativo!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);


        }

        [Fact]
        public async Task Execute_WhenToReturnLoanDataIncorrect_ReturnsError()
        {
            var request = new ReturnLoanCommandBuilder().Build();
            var book = new BookBuilder().WithId(request.IdBook).Build();
            var loan = new LoanBuilder()
                        .WithId(request.IdLoan)
                        .WithIdBook(request.IdBook+1)
                        .WithIdUser(request.IdUser)
                        .WithActive(false)
                        .Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(true);

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _repository.Setup(l => l.GetById(It.IsAny<int>())).ReturnsAsync(loan);

            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Dados para devolução do emprestimo incorretos!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);


        }

        [Fact]
        public async Task Execute_WhenToReturnLoanNotFound_ReturnsError()
        {
            var request = new ReturnLoanCommandBuilder().Build();
            var book = new BookBuilder().WithId(request.IdBook).Build();
            

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(true);

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _repository.Setup(l => l.GetById(It.IsAny<int>())).ReturnsAsync((Loan)null);

            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Emprestimo não encontrado!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Execute_WhenToReturnLoanNotFoundBook_ReturnsError()
        {
            var request = new ReturnLoanCommandBuilder().Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(true);

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync((Book)null);


            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Livro não encontrado!");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task Execute_WhenToReturnLoanNotFoundUser_ReturnsError()
        {
            var request = new ReturnLoanCommandBuilder().Build();

            _unitOfWork.Setup(uow => uow.BeginTransactionAsync());

            _userRepository.Setup(u => u.Exist(It.IsAny<int>())).ReturnsAsync(false);


            var response = new ReturnLoanHandler(_repository.Object, _userRepository.Object, _bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Usuário não encontrado");

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _userRepository.Verify(r => r.Exist(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _repository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);

            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Never);


        }

    }
}
