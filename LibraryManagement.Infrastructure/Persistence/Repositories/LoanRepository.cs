﻿using Azure.Core;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryManagementDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LoanRepository(LibraryManagementDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _unitOfWork.CompleteAsync();

            return loan.Id;
        }

        public async Task<bool> ExistsUser(int id)
        {
            return await _context.Loans.AnyAsync(l => l.IdUser == id && l.Active);
        }

        public async Task<bool> ExistsBook(int id)
        {
            return await _context.Loans.AnyAsync(l => l.IdBook == id && l.Active);
        }

        public async Task<List<Loan>> GetAll()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active).ToListAsync();
        }

        public async Task<List<Loan>> GetAllBookByUserLoan(int idUser, int idBook)
        {
            return await _context.Loans
                .Include(l => l.Book)
            .Include(l => l.User)
            .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser && c.IdBook == idBook).ToListAsync();
        }

        public async Task<List<Loan>> GetAllUserLoan(int idUser)
        {
            return await _context.Loans
            .Include(l => l.Book)
                .Include(l => l.User)
                .Where(c => !c.IsDeleted && c.Active && c.IdUser == idUser).ToListAsync();
        }

        public async Task<Loan?> GetById(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.Id == id && l.Active);
        }

        public async Task Update(Loan loan)
        {
            _context.Loans.Update(loan);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<List<Loan>> GetAllLoanDelay(int returnDays)
        {
            var dataOfLoan = _context.Loans.FirstOrDefault().DateOfLoan.AddDays(returnDays);
            var existeAtraso = (_context.Loans.FirstOrDefault().DateOfLoan < _context.Loans.FirstOrDefault().DateOfLoan.AddDays(returnDays));

            var loans = _context.Loans
                .Include(b => b.Book)
                .Include(l => l.User)
                .Where(l => l.Active && !l.IsDeleted && (l.DateOfLoan < l.DateOfLoan.AddDays(returnDays)));

            return loans.ToList();
        }
    }
}
