using LibraryManagement.Api.Entities;

namespace LibraryManagement.Api.Dtos.Books
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int YearPublished { get; set; }
        public int Quantity { get; set; }

        public Book ToEntity()
            => new Book(Title, Author, Isbn, YearPublished, Quantity);
    }
}
