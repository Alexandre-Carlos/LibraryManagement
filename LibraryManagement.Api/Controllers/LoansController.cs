using Azure;
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
    [Produces("application/json")]
    public class LoansController : ControllerBase
    {
        private readonly LibraryManagementDbContext _context;
        private readonly int _returnDays;

        public LoansController(LibraryManagementDbContext context, IOptions<ReturnDaysConfig> options)
        {
            _context = context;
            _returnDays = options.Value.Default;
        }

        /// <summary>
        /// Realizar o empréstimo de um livro
        /// </summary>
        /// <param name="model">LoanRequestDto</param>
        /// <returns>Dados de Emprestimo salvo</returns>
        [HttpPost]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Loan([FromBody] LoanRequestDto model)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == model.IdBook && b.Quantity > 0);
            var user = _context.Users.SingleOrDefault(u => u.Id == model.IdUser);

            if(user is null) return NotFound("Usuário não encontrado");

            if (book is null) return NotFound("Livro não encontrado ou não disponível para emprestimo!");

            var loan = model.ToEntity(_returnDays);

            book.SetLoanQuantity();

            _context.Books.Update(book);

            _context.Loans.Add(loan);
            _context.SaveChanges();

            var response = LoanResponseDto.FromEntity(loan);

            return CreatedAtAction(nameof(GetLoanByBookIdAndUserId), new { response.IdUser, response.IdBook }, response);
        }

        /// <summary>
        /// Listar todos os livros emprestados
        /// </summary>
        /// <returns>lista de todos os livros emprestados</returns>
        [HttpGet]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllLoan()
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active);

            var response =loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }

        /// <summary>
        /// Listar todos os livros emprestados por usuário
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>lista de livros emprestados para um usuário</returns>
        [HttpGet("{idUser}")]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUserLoan(int idUser)
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }

        /// <summary>
        /// Listar Livro emprestado para um usuário
        /// </summary>
        /// <param name="idUser">Identificação do usuário</param>
        /// <param name="idBook">Identificação do Livro emprestado</param>
        /// <returns>Informaç~eso do livro emprestado</returns>
        [HttpGet("{idUser},{idBook}")]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLoanByBookIdAndUserId(int idUser, int idBook) 
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser && c.IdBook == idBook);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l));

            return Ok(response);
        }
        /// <summary>
        /// Devolução de emprestimo
        /// </summary>
        /// <param name="id">Identificados do Emprestimo</param>
        /// <param name="model">Dados do Livro e Usuário</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
