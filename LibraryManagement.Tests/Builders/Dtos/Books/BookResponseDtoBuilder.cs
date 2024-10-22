using Bogus;
using LibraryManagement.Application.Dtos.Books;

namespace LibraryManagement.Tests.Builders.Dtos.Books
{
    public class BookResponseDtoBuilder
    {
        private readonly Faker<BookResponseDto> instance;

        public BookResponseDtoBuilder()
        {
            instance = new AutoFaker<BookResponseDto>();
        }

        public BookResponseDtoBuilder WithId(int id) 
        {
            instance.RuleFor(b => b.Id, id);
            return this;
        }
        public BookResponseDtoBuilder WithTitle(string title)
        {
            instance.RuleFor(b => b.Title, title);
            return this;
        }
        public BookResponseDtoBuilder WithAuthor(string author)
        {
            instance.RuleFor(b => b.Author, author);
            return this;
        }

        public BookResponseDtoBuilder WithIsbn(string isbn)
        {
            instance.RuleFor(b => b.Isbn, isbn);
            return this;
        }

        public BookResponseDtoBuilder WithYearPublished(int yearPublish)
        {
            instance.RuleFor(b => b.YearPublished, yearPublish);
            return this;
        }

        public BookResponseDtoBuilder WithQuantity(int quantity)
        {
            instance.RuleFor(b => b.Quantity, quantity);
            return this;
        }


        public BookResponseDto Build() => instance.Generate();
    }
}
