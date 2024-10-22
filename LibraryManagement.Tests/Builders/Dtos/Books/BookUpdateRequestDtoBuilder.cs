using Bogus;
using LibraryManagement.Application.Dtos.Books;

namespace LibraryManagement.Tests.Builders.Dtos.Books
{
    public class BookUpdateRequestDtoBuilder
    {
        private readonly Faker<BookUpdateRequestDto> instance;

        public BookUpdateRequestDtoBuilder()
        {
            instance = new AutoFaker<BookUpdateRequestDto>();
        }

        public BookUpdateRequestDtoBuilder WithId(int id)
        {
            instance.RuleFor(b => b.Id, id);
            return this;
        }
        public BookUpdateRequestDtoBuilder WithTitle(string title)
        {
            instance.RuleFor(b => b.Title, title);
            return this;
        }
        public BookUpdateRequestDtoBuilder WithAuthor(string author)
        {
            instance.RuleFor(b => b.Author, author);
            return this;
        }

        public BookUpdateRequestDtoBuilder WithIsbn(string isbn)
        {
            instance.RuleFor(b => b.Isbn, isbn);
            return this;
        }

        public BookUpdateRequestDtoBuilder WithYearPublished(int yearPublish)
        {
            instance.RuleFor(b => b.YearPublished, yearPublish);
            return this;
        }


        public BookUpdateRequestDto Build() => instance.Generate();
    }
}
