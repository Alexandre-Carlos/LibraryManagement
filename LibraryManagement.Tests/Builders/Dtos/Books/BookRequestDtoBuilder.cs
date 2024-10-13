using Bogus;
using LibraryManagement.Application.Dtos.Books;

namespace LibraryManagement.Tests.Builders.Dtos.Books
{
    public class BookRequestDtoBuilder
    {
        private readonly Faker<BookRequestDto> instance;

        public BookRequestDtoBuilder()
        {
            instance = new AutoFaker<BookRequestDto>();
        }

        public BookRequestDtoBuilder WithTitle (string title)
        {
            instance.RuleFor(b => b.Title,title);
            return this;
        }
        public BookRequestDtoBuilder WithAuthor(string author) 
        {
            instance.RuleFor(b => b.Author,author);
            return this;
        }

        public BookRequestDtoBuilder WithIsbn(string isbn)
        { 
            instance.RuleFor(b => b.Isbn,isbn);
            return this;
        }

        public BookRequestDtoBuilder WithYearPublished(int yearPublish)
        {
            instance.RuleFor(b => b.YearPublished,yearPublish);
            return this;
        }

        public BookRequestDtoBuilder WithQuantity(int quantity)
        {
            instance.RuleFor(b => b.Quantity,quantity);
            return this;
        }

        public BookRequestDto Build() => instance.Generate();
    }
}
