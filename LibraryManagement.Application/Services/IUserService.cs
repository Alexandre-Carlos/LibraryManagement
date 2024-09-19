using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace LibraryManagement.Application.Services
{
    public interface IUserService
    {
        ResultViewModel<List<UserResponseDto>> GetAll();
        ResultViewModel<UserResponseDto> GetById(int id);
        ResultViewModel<int> Insert(UserRequestDto request);
        ResultViewModel Update(int id ,UserRequestDto request);
        ResultViewModel DeleteById(int id);
    }
}
