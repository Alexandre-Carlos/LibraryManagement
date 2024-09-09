using LibraryManagement.Api.Configuration;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Api.Entities
{
    public class Loan : BaseEntity
    {

        public Loan(IOptions<ReturnDaysConfig> options, int idUser, int idBook, DateTime dateOfLoan, DateTime endDateLoan, List<Book> books)
        {
            IdUser = idUser;

            DateOfLoan = dateOfLoan;
            EndDateLoan = DateTime.Now.AddDays(options.Value.Default);
            Active = true; 
        }

        public int IdUser { get; private set; }
        public int IdBook { get; private set; }
        public User User { get; private set; }
        public Book Book { get; private set; }
        public DateTime DateOfLoan { get; private set; }
        public DateTime EndDateLoan { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public int DaysOfDelay { get; private set; }
        public bool Active { get; private set; }

        public void SetReturnDate()
        {
            ReturnDate = DateTime.Now;
            TimeSpan date = DateTime.Now - DateOfLoan;
            SetDaysOfDelay(date.Days);
        }

        public void SetDaysOfDelay(int daysOfDelay) => DaysOfDelay = daysOfDelay;
    }
}
