﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public interface ILoanRepository
    {
        Task<int> Add(Loan loan);
        Task Update(Loan loan);
        Task<List<Loan>> GetAll();
        Task<Loan?> GetById(int id);
        Task<bool> ExistsBook(int id);
        Task<bool> ExistsUser(int id);
        Task<List<Loan>> GetAllUserLoan(int idUser);
        Task<List<Loan>> GetAllBookByUserLoan(int idUser, int idBook);
        Task<List<Loan>> GetAllLoanDelay(int returnDays);
    }
}
