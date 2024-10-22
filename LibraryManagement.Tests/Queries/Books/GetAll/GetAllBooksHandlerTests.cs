using FluentAssertions;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Application.Queries.Books.GetAll;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Tests.Builders.Entities;
using Moq;

namespace LibraryManagement.Tests.Queries.Books.GetAll
{
    public class GetAllBooksHandlerTests
    {
        private readonly Mock<IBookRepository> _repository;

        public GetAllBooksHandlerTests()
        {
            _repository = new Mock<IBookRepository>();
        }

        [Fact]
        public async Task Execute_WhenToGetAllBook_ReturnsSuccess()
        {
            var request = new GetAllBooksQuery();
            var books = new List<Book> { new BookBuilder().Build(), new BookBuilder().Build() };

            var responseBookDto = books.Select(b => BookResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b =>b.GetAll()).ReturnsAsync(books);

            var response = new GetAllBooksHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Execute_WhenToGetAllBookEmpty_ReturnsSuccess()
        {
            var request = new GetAllBooksQuery();
            var books = new List<Book>();

            var responseBookDto = books.Select(b => BookResponseDto.FromEntity(b)).ToList();

            _repository.Setup(b => b.GetAll()).ReturnsAsync(books);

            var response = new GetAllBooksHandler(_repository.Object);

            var result = await response.Handle(request, new CancellationToken());

            result.IsSuccess.Should().BeTrue();

            result.Data.Should().BeEmpty();
        }

    }
}
