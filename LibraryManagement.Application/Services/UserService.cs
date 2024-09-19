using LibraryManagement.Application.Dtos;
using LibraryManagement.Application.Dtos.Users;
using LibraryManagement.Infrastructure.Persistence;

namespace LibraryManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryManagementDbContext _context;

        public UserService(LibraryManagementDbContext context)
        {
            _context = context;
        }
        public ResultViewModel DeleteById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            var loans = _context.Loans.Any(x => x.IdUser == id && x.Active);

            if (loans)
                return ResultViewModel.Error("Usuário ainda tem emprestimos ativos, não é possível realizar a operação!");

            user.SetAsDeleted();
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<UserResponseDto>> GetAll()
        {
            var user = _context.Users.ToList();

            var response = user.Select(u => UserResponseDto.FromEntity(u)).ToList();

            return ResultViewModel<List<UserResponseDto>>.Sucess(response);
        }

        public ResultViewModel<UserResponseDto> GetById(int id)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Id == id);

            if (user is null) return ResultViewModel<UserResponseDto>.Error("Usuário não encontrado");

            var response = UserResponseDto.FromEntity(user);

            return ResultViewModel<UserResponseDto>.Sucess(response);
        }

        public ResultViewModel<int> Insert(UserRequestDto request)
        {
            var user = request.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();

            var response = UserResponseDto.FromEntity(user);

            return ResultViewModel<int>.Sucess(response.Id);
        }

        public ResultViewModel Update(int id, UserRequestDto request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null) return ResultViewModel.Error("Usuário não encontrado");

            user.SetName(request.Name);
            user.SetEmail(request.Email);

            _context.Users.Update(user);
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
