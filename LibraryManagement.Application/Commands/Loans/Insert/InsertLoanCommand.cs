using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Entities;
using MediatR;

namespace LibraryManagement.Application.Commands.Loans.Insert
{
    public class InsertLoanCommand : IRequest<ResultViewModel<int>>
    {
        public int IdUser { get; set; }
        public int IdBook { get; set; }


        public Loan ToEntity(int returnDays) => new Loan(IdUser, IdBook, returnDays);
    }
}
