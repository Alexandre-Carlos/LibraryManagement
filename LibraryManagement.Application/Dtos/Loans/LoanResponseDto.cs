using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Loans
{
    public class LoanResponseDto
    {
        public LoanResponseDto() {}
        public LoanResponseDto(int id, int idUser, string fullNameUser, int idBook, string titleBook, 
            DateTime dateOfLoan, DateTime endDateLoan)
        {
            Id = id;
            IdUser = idUser;
            FullNameUser = fullNameUser;
            IdBook = idBook;
            TitleBook = titleBook;

            DateOfLoan = dateOfLoan;
            EndDateLoan = endDateLoan;
        }

        public int Id { get; private set; }
        public int IdUser { get; private set; }
        public string FullNameUser { get; private set; }
        public int IdBook { get; private set; }
        public string TitleBook { get; private set; }
        public DateTime DateOfLoan { get; private set; }
        public DateTime EndDateLoan { get; private set; }

        
        public static LoanResponseDto FromEntity(Loan entity)
        {
            return new(entity.Id, entity.User.Id, entity.User.Name, entity.Book.Id, entity.Book.Title, entity.DateOfLoan, entity.EndDateLoan);
        }


    }
}
