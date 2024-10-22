using Bogus;
using LibraryManagement.Application.Commands.Books.Update;

namespace LibraryManagement.Tests.Builders.Command.Books.Update
{
    public class UpdateBookCommandBuilder
    {
        public readonly Faker<UpdateBookCommand> instance;

        public UpdateBookCommandBuilder()
        {
            instance = new AutoFaker<UpdateBookCommand>();
        }

        public UpdateBookCommandBuilder WithId(int id)
        {
            instance.RuleFor(x => x.Id, id);
            return this;
        }
        public UpdateBookCommandBuilder WithTitle(string title)
        {
            instance.RuleFor(x => x.Title, title);
            return this;
        }
        public UpdateBookCommandBuilder WithAuthor(string author)
        {
            instance.RuleFor(x => x.Author, author);
            return this;
        }
        public UpdateBookCommandBuilder WithIsbn(string isbn)
        {
            instance.RuleFor(x => x.Isbn, isbn);
            return this;
        }
        public UpdateBookCommandBuilder WithYearPublished(int yearPublished)
        {
            instance.RuleFor(x => x.YearPublished, yearPublished);
            return this;
        }

        public UpdateBookCommand Build() => instance.Generate();
    }
}
