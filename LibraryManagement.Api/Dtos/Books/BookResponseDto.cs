using LibraryManagement.Api.Entities;

namespace LibraryManagement.Api.Dtos.Books
{
    public class BookResponseDto
    {
        public BookResponseDto(int id, string titulo, string author, int yearPublished, int quantity)
        {
            Id = id;
            Titulo = titulo;
            Author = author;
            YearPublished = yearPublished;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Author { get; private set; }
        public int YearPublished { get; private set; }
        public int Quantity { get; private set; }


        public static BookResponseDto FromEntity(Book entity)
        => new (entity.Id, entity.Titulo, entity.Author, entity.YearPublished, entity.Quantity);
        
    }

}
