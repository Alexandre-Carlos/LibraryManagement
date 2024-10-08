﻿using LibraryManagement.Api.Configuration;
using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Loans;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Application.Commands.Loans.Insert
{
    public class InsertLoanHandler : IRequestHandler<InsertLoanCommand, ResultViewModel<int>>
    {
        private readonly ILoanRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _returnDays;

        public InsertLoanHandler(ILoanRepository repository, 
            IBookRepository bookRepository, 
            IUserRepository userRepository, 
            IOptions<ReturnDaysConfig> options, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _returnDays = options.Value.Default;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<int>> Handle(InsertLoanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var book = await _bookRepository.GetByIdAndHasQuantity(request.IdBook);

                if (book is null) return ResultViewModel<int>.Error("Livro não encontrado ou não disponível para emprestimo!");

                var user = await _userRepository.GetById(request.IdUser);

                if (user is null) return ResultViewModel<int>.Error("Usuário não encontrado");

                var loan = request.ToEntity(_returnDays);

                book.SetLoanQuantity();

                await _bookRepository.Update(book);

                //throw new ArgumentException("Erro na gravação do livro.");

                await _repository.Add(loan);

                //throw new ArgumentException("Erro na gravação do emprestimo.");

                var response = LoanResponseDto.FromEntity(loan);

                await _unitOfWork.CommitAsync();

                return ResultViewModel<int>.Sucess(response.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
