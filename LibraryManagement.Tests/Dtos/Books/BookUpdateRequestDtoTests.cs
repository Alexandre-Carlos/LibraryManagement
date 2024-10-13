using FluentAssertions;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Entities;
using LibraryManagement.Tests.Builders.Dtos.Books;
using LibraryManagement.Tests.Builders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Dtos.Books
{
    public class BookUpdateRequestDtoTests
    {
        [Fact]
        public void Create_BookUpdateRequestDtoIsOk_Success()
        {
            var bookUpdateRequestDto = new BookUpdateRequestDtoBuilder().Build();

            bookUpdateRequestDto.Should().NotBeNull();
        }

        [Fact]
        public void Returns_BookEntityIsOk_Success()
        {
            var bookUpdateRequestDto = new BookUpdateRequestDtoBuilder().Build();

            var book = bookUpdateRequestDto.ToEntity();

            book.Should().NotBeNull();
            book.Should().GetType().Equals(typeof(Book));

            book.Title.Should().Be(bookUpdateRequestDto.Title);
            book.Author.Should().Be(bookUpdateRequestDto.Author);
            book.Isbn.Should().Be(bookUpdateRequestDto.Isbn);
            book.YearPublished.Should().Be(bookUpdateRequestDto.YearPublished);
        }
    }
}
