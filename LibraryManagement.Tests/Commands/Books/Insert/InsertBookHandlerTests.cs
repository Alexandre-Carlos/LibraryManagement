using FluentAssertions;
using LibraryManagement.Application.Commands.Books.Insert;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Tests.Builders.Command.Books.Insert;
using Moq;
using Moq.AutoMock;

namespace LibraryManagement.Tests.Commands.Books.Insert
{
    public class InsertBookHandlerTests
    {
        private readonly AutoMocker _autoMocker = new();
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IBookRepository> _bookRepository;

        public InsertBookHandlerTests()
        {
            _unitOfWork = _autoMocker.GetMock<IUnitOfWork>();
            _bookRepository = _autoMocker.GetMock<IBookRepository>();
        }

        [Fact]
        public async Task Execute_WhenToInsertBook_ReturnsSuccess()
        {
            var request = new InsertBookCommandBuilder().Build();

            var book = request.ToEntity();

            _unitOfWork.Setup(x => x.BeginTransactionAsync());

            _bookRepository.Setup(b => b.Add(book)).ReturnsAsync(It.IsAny<int>());

            _unitOfWork.Setup(x => x.CommitAsync());

            var insertBookHandler = new InsertBookHandler(_bookRepository.Object, _unitOfWork.Object);

            var bookResponse = BookResponseDto.FromEntity(book);   

            var result = await insertBookHandler.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            _bookRepository.Verify(b => b.Add(It.IsAny<Book>()), Times.Once());
            _unitOfWork.Verify(u => u.BeginTransactionAsync(), Times.Once());
            _unitOfWork.Verify(u => u.CommitAsync(), Times.Once());
        }
    }
}
