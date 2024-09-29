using FluentAssertions;
using LibraryManagement.Tests.Builders.Entities;

namespace LibraryManagement.Tests.Entities
{
    public class BookTests
    {
        [Fact]
        public void Update_BookDataIsOk_Success()
        {
            //Arrange
            var title = "Design Patterns";
            var author = "Eric Evans";
            var isbn = "1252845895213";
            var yearPublished = 2015;
            

            var book = new BookBuilder().Build();
            //Act

            book.Update(title, author, isbn, yearPublished);

            //Assert

            Assert.Equal(title, book.Title);
            Assert.Equal(author, book.Author);
            Assert.Equal(isbn, book.Isbn);
            Assert.Equal(yearPublished, book.YearPublished);

            book.Title.Should().BeEquivalentTo(title);
            book.Author.Should().BeEquivalentTo(author);
            book.Isbn.Should().BeEquivalentTo(isbn);
            book.YearPublished.Should().Be(yearPublished);
        }

        [Fact]
        public void Update_BookQuantityIsOk_Success()
        {
            //Arrange
            var title = "Design Patterns";
            var author = "Eric Evans";
            var isbn = "1252845895213";
            var yearPublished = 2015;
            var quantity = 2;

            var book = new BookBuilder().Build();
            var quantidadeAtualizada = quantity + book.Quantity;

            //Act
            book.Update(title, author, isbn, yearPublished);
            book.SetAddQuantity(quantity);

            //Assert

            book.Title.Should().BeEquivalentTo(title);
            book.Author.Should().BeEquivalentTo(author);
            book.Isbn.Should().BeEquivalentTo(isbn);
            book.YearPublished.Should().Be(yearPublished);
            book.Quantity.Should().Be(quantidadeAtualizada);

        }

        [Fact]
        public void Update_BookIsNotOk_QuantityWrong()
        {
            var quantity = 2;

            var book = new BookBuilder().Build();

            book.SetIncrementQuantity();

            var quantidadeAtualizada = quantity + book.Quantity;

            book.SetDecrementQuantity();

            book.SetAddQuantity(quantity);

            book.Quantity.Should().NotBe(quantidadeAtualizada);

        }

        [Fact]
        public void Update_BookQuantityDecrementIsNotOk_ThrowExeception()
        {
            var book = new BookBuilder().Build();

            book.SetDecrementQuantity();

            var exception = Assert.Throws<InvalidOperationException>(() =>
                book.SetDecrementQuantity()
            );

            Assert.Equal("Quantidade indisponível!", exception.Message);

            book.Invoking(b => b.SetDecrementQuantity())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Quantidade indisponível!");
        }

        [Fact]
        public void Change_BookDataBySetsIsOk_Success()
        {
            //Arrange
            var title = "Design Patterns";
            var author = "Eric Evans";
            var isbn = "1252845895213";
            var yearPublished = 2015;


            var book = new BookBuilder().Build();
            //Act

            book.SetTitle(title);
            book.SetAuthor(author);
            book.SetIsbn(isbn);
            book.SetYearPublished(yearPublished);

            //Assert
            book.Title.Should().BeEquivalentTo(title);
            book.Author.Should().BeEquivalentTo(author);
            book.Isbn.Should().BeEquivalentTo(isbn);
            book.YearPublished.Should().Be(yearPublished);
        }

        [Fact]
        public void Check_BookIsDeletecOk_Success()
        {
            var book = new BookBuilder().Build();

            book.SetAsDeleted();

            book.IsDeleted.Should().BeTrue();
            
        }

    }
}