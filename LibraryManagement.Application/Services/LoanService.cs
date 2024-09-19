using LibraryManagement.Api.Configuration;
using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace LibraryManagement.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryManagementDbContext _context;
        private readonly int _returnDays;

        public LoanService(LibraryManagementDbContext context, IOptions<ReturnDaysConfig> options)
        {
            _context = context;
            _returnDays = options.Value.Default;
        }

        public ResultViewModel<List<LoanResponseDto>> GetAll()
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }

        public ResultViewModel<List<LoanResponseDto>> GetAllUserLoan(int idUser)
        {
            var loans = _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }

        public ResultViewModel<LoanResponseDto> GetById(int id)
        {
            var loan = _context.Loans
                .Include (l => l.Book)
                .Include(l => l.User)
                .SingleOrDefault(l => l.Id == id);

            if (loan is null) return ResultViewModel<LoanResponseDto>.Error("Emprestimo não localizado");

            var response = LoanResponseDto.FromEntity(loan);

            return ResultViewModel<LoanResponseDto>.Sucess(response);
        }

        public ResultViewModel<List<LoanResponseDto>> GetLoanByBookIdAndUserId(int bookId, int userId)
        {
            var loans = _context.Loans
                .Include(l => l.Book)
            .Include(l => l.User)
            .Where(c => !c.IsDeleted && c.Active && c.IdUser == userId && c.IdBook == bookId);

            var response = loans.Select(l => LoanResponseDto.FromEntity(l)).ToList();

            return ResultViewModel<List<LoanResponseDto>>.Sucess(response);
        }

        public ResultViewModel<int> Insert(LoanRequestDto request)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == request.IdBook && b.Quantity > 0);
            var user = _context.Users.SingleOrDefault(u => u.Id == request.IdUser);

            if (user is null) return ResultViewModel<int>.Error("Usuário não encontrado");

            if (book is null) return ResultViewModel<int>.Error("Livro não encontrado ou não disponível para emprestimo!");

            var loan = request.ToEntity(_returnDays);

            book.SetLoanQuantity();

            _context.Books.Update(book);

            _context.Loans.Add(loan);
            _context.SaveChanges();

            var response = LoanResponseDto.FromEntity(loan);

            return ResultViewModel<int>.Sucess(response.Id);
        }

        public ResultViewModel<string> ReturnLoan(int id, LoanReturnRequestDto request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == request.IdUser);
            if (user is null) return ResultViewModel<string>.Error("Usuário não encontrado");

            var book = _context.Books.SingleOrDefault(b => b.Id == request.IdBook);
            if (book is null) return ResultViewModel<string>.Error("Livro não encontrado ou não disponível para emprestimo!");

            var loan = _context.Loans.SingleOrDefault(l => l.Id == id);
            if (loan is null) return ResultViewModel<string>.Error("Emprestimo não encontrado!");

            loan.SetReturnDate();
            book.SetDevolutionQuantity();

            _context.Books.Update(book);
            _context.Loans.Update(loan);
            _context.SaveChanges();

            return ResultViewModel<string>.Sucess("Devolução realizada com sucesso!");
        }
    }
}
