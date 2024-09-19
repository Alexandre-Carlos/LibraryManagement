using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Route("api/Loans")]
    [Produces("application/json")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        /// <summary>
        /// Realizar o empréstimo de um livro
        /// </summary>
        /// <param name="model">LoanRequestDto</param>
        /// <returns>Dados de Emprestimo salvo</returns>
        [HttpPost]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Loan([FromBody] LoanRequestDto request)
        {
            var response = _loanService.Insert(request);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return CreatedAtAction(nameof(GetById), new { response }, response.Data);
        }

        /// <summary>
        /// Listar todos os livros emprestados
        /// </summary>
        /// <returns>lista de todos os livros emprestados</returns>
        [HttpGet]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllLoan()
        {
            var response = _loanService.GetAll();

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }

        /// <summary>
        /// Buscar um livro emprestado
        /// </summary>
        /// <returns>Buscar o livro emprestado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            var response = _loanService.GetById(id);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }

        /// <summary>
        /// Listar todos os livros emprestados por usuário
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns>lista de livros emprestados para um usuário</returns>
        [HttpGet("user/{idUser}")]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUserLoan(int idUser)
        {
            var response = _loanService.GetAllUserLoan(idUser);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }

        /// <summary>
        /// Listar Livro emprestado para um usuário
        /// </summary>
        /// <param name="idUser">Identificador do usuário</param>
        /// <param name="idBook">Identificador do Livro emprestado</param>
        /// <returns>Informaç~eso do livro emprestado</returns>
        [HttpGet("{idUser},{idBook}")]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLoanByBookIdAndUserId(int idUser, int idBook) 
        {
            var response = _loanService.GetLoanByBookIdAndUserId(idBook, idUser);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return Ok(response);
        }
        /// <summary>
        /// Devolução de emprestimo
        /// </summary>
        /// <param name="id">Identificador do Emprestimo</param>
        /// <param name="model">Dados do Livro e Usuário</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ReturnLoan(int id, [FromBody] LoanReturnRequestDto model)
        {
            var response = _loanService.ReturnLoan(id, model);

            if (!response.IsSucess)
                return BadRequest(response.Message);

            return NoContent();
        }
    }
}
