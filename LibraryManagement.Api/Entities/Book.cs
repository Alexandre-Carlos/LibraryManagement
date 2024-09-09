namespace LibraryManagement.Api.Entities
{
    public class Book : BaseEntity
    {
        public Book() { }
        public Book(string title, string author, int yearPublished, int quantity) : base()
        {
            Title = title;
            Author = author;
            YearPublished = yearPublished;
            Quantity = quantity;
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public int YearPublished { get; private set; }
        public int Quantity { get; private set; }

        public List<Loan> Loans {  get; private set; }

        public void SetAddQuantity(int quantity) => Quantity += quantity;

        public void SetReduceQuantity(int quantity) => Quantity += quantity;
    }
}
