using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Books;

namespace LibraryManagement.Application.Services
{
    public interface IBookService
    {
        ResultViewModel<List<BookResponseDto>> GetAll();
        ResultViewModel<BookResponseDto> GetById(int id);
        ResultViewModel<int> Insert(BookRequestDto request);
        ResultViewModel Update(int id, BookUpdateRequestDto request);
        ResultViewModel Delete(int id);
    }
}
