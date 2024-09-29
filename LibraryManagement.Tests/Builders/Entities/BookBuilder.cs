using Bogus;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Tests.Builders.Entities
{
    public class BookBuilder
    {
        private readonly Faker<Book> instance;

        public BookBuilder()
        {
            instance = new AutoFaker<Book>();
        }

        public BookBuilder WithTitle(string title) 
        {
            instance.RuleFor(b => b.Title, title);
            return this;
        }

        public BookBuilder WithAuthor(string author)
        {
            instance.RuleFor(b => b.Author, author); 
            return this;
        }

        public BookBuilder WithIsbn(string isbn) 
        {
            instance.RuleFor(b => b.Isbn, isbn);
            return this;
        }

        public BookBuilder WithQuantity(int quantity) 
        {
            instance.RuleFor(b => b.Quantity, quantity);
            return this;
        }

        public BookBuilder WithYearPublished(int yearPublished)
        {
            instance.RuleFor(b => b.YearPublished, yearPublished);
            return this;
        }
        public Book Build() => instance.Generate();


    }
}
