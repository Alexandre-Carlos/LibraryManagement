using LibraryManagement.Api.Dtos.Loan;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Loans")]
    public class LoansController : ControllerBase
    {
        [HttpPost]
        public IActionResult Loan([FromBody] LoanRequestDto model)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult ReturnLoan(int id, [FromBody] LoanReturnRequestDto model)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllLoan()
        {
            return Ok();
        }

        [HttpGet("{idUser}")]
        public IActionResult GetAllUserLoan(int idUser)
        {
            return Ok();
        }

        [HttpGet("{idUser},{idBook}")]
        public IActionResult GetLoanByBookIdAndUserId(int idUser, int idBook) 
        { 
            return Ok(); 
        }
    }
}
