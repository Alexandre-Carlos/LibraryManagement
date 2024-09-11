using LibraryManagement.Api.Configuration;
using LibraryManagement.Api.Dtos.Loans;
using LibraryManagement.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Loans")]
    public class LoansController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;
        private readonly int _returnDays;

        public LoansController(LibraryManagementDbContext context, IOptions<ReturnDaysConfig> options)
        {
            _context = context;
            _returnDays = options.Value.Default;
        }

        [HttpPost]
        public IActionResult Loan([FromBody] LoanRequestDto model)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == model.IdBook && b.Quantity > 0);
            var user = _context.Users.SingleOrDefault(u => u.Id == model.IdUser);

            if(user is null) return NotFound("Usuário não encontrado");

            if (book is null) return NotFound("Livro não encontrado ou não disponível para emprestimo!");

            var response = model.ToEntity(_returnDays);

            book.SetLoanQuantity();

            _context.Books.Update(book);

            _context.Loans.Add(response);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllLoan()
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active);

            var response =loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }

        [HttpGet("{idUser}")]
        public IActionResult GetAllUserLoan(int idUser)
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }

        [HttpGet("{idUser},{idBook}")]
        public IActionResult GetLoanByBookIdAndUserId(int idUser, int idBook) 
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser && c.IdBook == idBook);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }

        [HttpPost("{id}")]
        public IActionResult ReturnLoan(int id, [FromBody] LoanReturnRequestDto model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == model.IdUser);
            if (user is null) return NotFound("Usuário não encontrado");

            var book = _context.Books.SingleOrDefault(b => b.Id == model.IdBook);
            if (book is null) return NotFound("Livro não encontrado ou não disponível para emprestimo!");

            var loan = _context.Loans.SingleOrDefault(l => l.Id == id);
            if (loan is null) return NotFound("Emprestimo não encontrado!");

            loan.SetReturnDate();
            book.SetDevolutionQuantity();

            _context.Books.Update(book);
            _context.Loans.Update(loan);
            _context.SaveChanges();

            return Ok("Devolução realizada com sucesso!");
        }
    }
}
