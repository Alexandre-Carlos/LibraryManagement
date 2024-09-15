using LibraryManagement.Api.Configuration;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Api.Entities
{
    public class Loan : BaseEntity
    {
        public Loan()
        {
            
        }
        public Loan(int idUser, int idBook, int returnDays )
        {
            IdUser = idUser;
            IdBook = idBook;
            DateOfLoan = DateTime.Now;
            EndDateLoan = DateTime.Now.AddDays(returnDays);
            Active = true;
        }

        public int IdUser { get; private set; }
        public int IdBook { get; private set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime DateOfLoan { get; private set; }
        public DateTime EndDateLoan { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public int DaysOfDelay { get; private set; }
        public bool Active { get; private set; }

        public void SetReturnDate()
        {
            ReturnDate = DateTime.Now;
            SetDaysOfDelay();
        }

        private void SetDaysOfDelay()
        {
            TimeSpan date = DateTime.Now - DateOfLoan;
            DaysOfDelay = date.Days;
        } 



    }
}
