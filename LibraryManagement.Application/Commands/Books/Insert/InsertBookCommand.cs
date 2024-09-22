using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Insert
{
    public class InsertBookCommand : IRequest<ResultViewModel<int>>
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
