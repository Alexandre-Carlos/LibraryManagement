using LibraryManagement.Application.Dtos.Users;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace LibraryManagement.Application.Services
{
    public interface IUserService
    {
        List<UserResponseDto> GetAll();
        UserResponseDto GetById(int id);
        UserResponseDto Insert(UserRequestDto request);
        UserResponseDto Update(int id ,UserRequestDto request);
        UserResponseDto DeleteById(int id);
    }
}
