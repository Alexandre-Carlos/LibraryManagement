using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Dtos.Loans
{
    public class LoanRequestDto
    {
        public LoanRequestDto() { }
        public int IdUser { get; set; }
        public int IdBook { get; set; }


        public Loan ToEntity(int returnDays) => new Loan(IdUser, IdBook, returnDays);
    }
}
