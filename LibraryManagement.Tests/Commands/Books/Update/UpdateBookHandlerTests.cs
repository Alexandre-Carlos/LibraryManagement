using FluentAssertions;
using LibraryManagement.Application.Commands.Books.Update;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Command.Books.Update;
using LibraryManagement.Tests.Builders.Entities;
using Moq;
using Moq.AutoMock;

namespace LibraryManagement.Tests.Commands.Books.Update
{
    public class UpdateBookHandlerTests
    {
        private readonly AutoMocker _autoMocker = new();
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookRepository> _bookRepository;

        public UpdateBookHandlerTests()
        {
            _unitOfWork = _autoMocker.GetMock<IUnitOfWork>();
            _bookRepository = _autoMocker.GetMock<IBookRepository>();
            
        }

        [Fact]
        public async void Execute_WhenToUpdateBook_ReturnsSuccess()
        {
            var updateCommand = new UpdateBookCommandBuilder().Build();

            var book = new BookBuilder().Build();

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            book.Update(updateCommand.Title, updateCommand.Author, updateCommand.Isbn, updateCommand.YearPublished);

            _unitOfWork.Setup(u => u.BeginTransactionAsync());

            _bookRepository.Setup(b => b.Update(It.IsAny<Book>()));

            _unitOfWork.Setup(u => u.CommitAsync());

            var response = new UpdateBookHandler(_bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(updateCommand, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
            _bookRepository.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async void Execute_WhenToUpdateBook_ReturnsError()
        {
            var updateCommand = new UpdateBookCommandBuilder().Build();

            Book ?book =  null;

            _bookRepository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            var response = new UpdateBookHandler(_bookRepository.Object, _unitOfWork.Object);

            var result = await response.Handle(updateCommand, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Livro não encontrado!");

           
            _bookRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
