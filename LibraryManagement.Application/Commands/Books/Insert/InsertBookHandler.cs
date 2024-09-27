using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using MediatR;

namespace LibraryManagement.Application.Commands.Books.Insert
{
    public class InsertBookHandler : IRequestHandler<InsertBookCommand, ResultViewModel<int>>
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public InsertBookHandler(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<int>> Handle(InsertBookCommand request, CancellationToken cancellationToken)
        {
            var book = request.ToEntity();

            await _unitOfWork.BeginTransactionAsync();

            await _repository.Add(book);

            await _unitOfWork.CommitAsync();

            var response = BookResponseDto.FromEntity(book);

            return ResultViewModel<int>.Sucess(response.Id);
        }
    }
}
