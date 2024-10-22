using LibraryManagement.Application.Dtos;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Update
{
    public  class UpdateBookCommand : IRequest<ResultViewModel>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int YearPublished { get; set; }
    }
}
