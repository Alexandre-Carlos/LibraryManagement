using FluentAssertions;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Tests.Builders.Dtos.Books;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Dtos.Books
{
    public class BookResponseDtoTests
    {
        [Fact]
        public void Create_BookResponseDtoIsOk_Success()
        {
            var bookResponseDto = new BookResponseDtoBuilder().Build();

            bookResponseDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_BookFromEntityIsOk_Success()
        {
            var book = new BookBuilder().Build();

            var bookResponseDto = BookResponseDto.FromEntity(book);

            bookResponseDto.Should().NotBeNull();

            bookResponseDto.Title.Should().Be(book.Title);
            bookResponseDto.Author.Should().Be(book.Author);
            bookResponseDto.Isbn.Should().Be(book.Isbn);
            bookResponseDto.YearPublished.Should().Be(book.YearPublished);
            bookResponseDto.Quantity.Should().Be(book.Quantity);

        }
    }
}
