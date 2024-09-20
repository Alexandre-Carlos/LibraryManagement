using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Books
{
    public class BookResponseDto
    {
        public BookResponseDto(int id, string title, string author, string isbn, int yearPublished, int quantity)
        {
            Id = id;
            Title = title;
            Author = author;
            Isbn = isbn;
            YearPublished = yearPublished;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Isbn { get; private set; }
        public int YearPublished { get; private set; }

        public int Quantity { get; private set; }

        public static BookResponseDto FromEntity(Book entity)
        => new(entity.Id, entity.Title, entity.Author, entity.Isbn, entity.YearPublished, entity.Quantity);

    }

}
