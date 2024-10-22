using LibraryManagement.Application.Commands.Loans.Insert;
using LibraryManagement.Application.Commands.Loans.Notify;
using LibraryManagement.Application.Commands.Loans.ReturnLoan;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Application.Queries.Loans.GetAll;
using LibraryManagement.Application.Queries.Loans.GetAllUserLoan;
using LibraryManagement.Application.Queries.Loans.GetById;
using LibraryManagement.Application.Queries.Loans.GetLoanByBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Loans")]
    [Produces("application/json")]
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Realizar o empréstimo de um livro
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Dados de Emprestimo salvo</returns>
        [HttpPost]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Loan([FromBody] InsertLoanCommand request)
        {
            var response = await _mediator.Send(request);

            if (!response.IsSuccess)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        /// <summary>
        /// Listar todos os livros emprestados
        /// </summary>
        /// <returns>lista de todos os livros emprestados</returns>
        [HttpGet]
        [ProducesResponseType<LoanResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLoan()
        {
            var response = await _mediator.Send(new GetAllLoanQuery());

            if (!response.IsSuccess)
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
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetLoanByIdQuery(id));

            if (!response.IsSuccess)
                return BadRequest(response);

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
        public async Task<IActionResult> GetAllUserLoan(int idUser)
        {
            var response = await _mediator.Send(new GetAllUserLoanQuery(idUser));

            if (!response.IsSuccess)
                return BadRequest(response);

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
        public async Task<IActionResult> GetLoanByBookIdAndUserId(int idUser, int idBook) 
        {
            var response = await _mediator.Send(new GetLoanByBookQuery(idBook, idUser));

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }
        /// <summary>
        /// Devolução de emprestimo
        /// </summary>
        /// <param name="id">Identificador do Emprestimo</param>
        /// <param name="request">Dados do Livro e Usuário</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ReturnLoan(int id, [FromBody] ReturnLoanCommand request)
        {
            var response = await _mediator.Send(request);

            if (!response.IsSuccess)
                return BadRequest(response);

            return NoContent();
        }
    }
}
