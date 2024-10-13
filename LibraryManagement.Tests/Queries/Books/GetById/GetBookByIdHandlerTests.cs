using FluentAssertions;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Application.Queries.Books.GetById;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Dtos.Books;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Books.GetById
{
    public class GetBookByIdHandlerTests
    {
        private readonly Mock<IBookRepository> _repository;

        public GetBookByIdHandlerTests()
        {
            _repository = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Execute_WhenGetByIdBook_ReturnsSuccess()
        {
            var request = new GetBookByIdQuery(1);

            var book = new BookBuilder().WithId(request.Id).Build();

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(book);

            var bookResponseDto = new BookResponseDtoBuilder().Build();

            var responseBookDto = BookResponseDto.FromEntity(book);

            var response = new GetBookByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenGetByIdBookNotFound_ReturnsError()
        {
            var request = new GetBookByIdQuery(1);

            _repository.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync((Book)null);
                       

            var response = new GetBookByIdHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeFalse();

            result.Message.Should().Be("Livro não encontrado");

            result.Data.Should().BeNull();

            _repository.Verify(b => b.GetById(It.IsAny<int>()), Times.Once);
        }
    }
}
