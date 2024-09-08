namespace LibraryManagement.Api.Dtos.Book
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int YearPublished { get; set; }
    }
}
