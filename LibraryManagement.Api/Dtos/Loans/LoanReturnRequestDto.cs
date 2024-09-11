namespace LibraryManagement.Api.Dtos.Loans
{
    public class LoanReturnRequestDto
    {
        public int IdLoan { get; set; }
        public int IdUser { get; set; }
        public int IdBook { get; set; }
    }
}
