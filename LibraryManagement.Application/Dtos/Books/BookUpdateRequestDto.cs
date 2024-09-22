using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Books
{
    public class BookUpdateRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int YearPublished { get; set; }

        public Book ToEntity()
            => new (Title,Author,Isbn,YearPublished);
    }
}
