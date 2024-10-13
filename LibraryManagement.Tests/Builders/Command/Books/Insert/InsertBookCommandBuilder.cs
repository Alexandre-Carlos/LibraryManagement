using Bogus;
using LibraryManagement.Application.Commands.Books.Insert;

namespace LibraryManagement.Tests.Builders.Command.Books.Insert
{
    public class InsertBookCommandBuilder
    {
        private readonly Faker<InsertBookCommand> instance;

        public InsertBookCommandBuilder()
        {
            instance = new AutoFaker<InsertBookCommand>();
        }

        public InsertBookCommandBuilder WithTitle(string title)
        { 
            instance.RuleFor(x => x.Title, title);
            return this;
        }

        public InsertBookCommandBuilder WithAuthor(string author)
        {
            instance.RuleFor(x => x.Author, author);
            return this;
        }

        public InsertBookCommandBuilder WithIsbn(string isbn)
        {
            instance.RuleFor(x => x.Isbn, isbn);
            return this;
        }

        public InsertBookCommandBuilder WithYearPublished(int yearPublished)
        {
            instance.RuleFor(x => x.YearPublished, yearPublished);
            return this;
        }

        public InsertBookCommandBuilder WithQuantity(int quantity)
        {
            instance.RuleFor(x => x.Quantity, quantity);
            return this;
        }

        public InsertBookCommand Build() => instance.Generate();
    }
}
