using FluentAssertions;
using LibraryManagement.Application.Commands.Books.Delete;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;
using Moq.AutoMock;

namespace LibraryManagement.Tests.Commands.Books.Delete
{
    public class DeleteBookHandlerTests
    {
        private readonly AutoMocker _autoMocker = new();
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<ILoanRepository> _loanRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DeleteBookHandlerTests()
        {
            _loanRepository = _autoMocker.GetMock<ILoanRepository>();
            _unitOfWork = _autoMocker.GetMock<IUnitOfWork>();
            _bookRepository = _autoMocker.GetMock<IBookRepository>();
        }

        [Fact]
        public async Task Execute_WhenToDeleteBook_ReturnsSuccess()
        {
            var request = new DeleteBookCommand(1);

            var book = new BookBuilder().Build();

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _loanRepository.Setup(l => l.ExistsBook(It.IsAny<int>())).ReturnsAsync(false);

            book.SetAsDeleted();


            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            _bookRepository.Setup(b => b.Update(It.IsAny<Book>()));

            

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new DeleteBookHandler(_bookRepository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);

            _loanRepository.Verify(r => r.ExistsBook(It.IsAny<int>()), Times.Once);


        }

        [Fact]
        public async Task Execute_WhenToDeleteBookExistLoan_ReturnsError()
        {
            var request = new DeleteBookCommand(1);

            var book = new BookBuilder().Build();

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            _loanRepository.Setup(l => l.ExistsBook(It.IsAny<int>())).ReturnsAsync(true);

            var response = new DeleteBookHandler(_bookRepository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);

            _loanRepository.Verify(r => r.ExistsBook(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenToDeleteBookNotFound_ReturnsError()
        {
            var request = new DeleteBookCommand(1);

            Book ?book = null;

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            var response = new DeleteBookHandler(_bookRepository.Object, _loanRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Livro não encontrado!");

  



        }
    }
}
