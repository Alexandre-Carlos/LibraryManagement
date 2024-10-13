using FluentAssertions;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Dtos.Books;

namespace LibraryManagement.Tests.Dtos.Books
{
    public class BookRequestDtoTests
    {
        [Fact]
        public void Create_BookRequestDtoIsOk_Success()
        {
            var bookRequestDto = new BookRequestDtoBuilder().Build();

            bookRequestDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_BookEntityIsOk_Success()
        {
            var bookResponseDto = new BookRequestDtoBuilder().Build();

            var book = bookResponseDto.ToEntity();

            book.Should().NotBeNull();
            book.Should().GetType().Equals(typeof(Book));

            book.Title.Should().Be(bookResponseDto.Title);
            book.Author.Should().Be(bookResponseDto.Author);
            book.Isbn.Should().Be(bookResponseDto.Isbn);
            book.YearPublished.Should().Be(bookResponseDto.YearPublished);
            book.Quantity.Should().Be(bookResponseDto.Quantity);

        }
    }
}
