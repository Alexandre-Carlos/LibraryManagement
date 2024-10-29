using LibraryManagement.Application.Dtos;
using LibraryManagement.Core.Account;
using MediatR;

namespace LibraryManagement.Application.Queries.Users.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, ResultViewModel<string>>
    {
        private readonly IAuthenticate _authenticate;
        
        public LoginUserHandler(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<ResultViewModel<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var isUserExist = await _authenticate.UserExist(request.Email);
            if (!isUserExist) return ResultViewModel<string>.Error("Erro no Login!");

            var authenticate = await _authenticate.AuthenticateAsync(request.Email, request.Password);

            if(string.IsNullOrEmpty(authenticate)) return ResultViewModel<string>.Error("Erro no Login!");

            return ResultViewModel<string>.Sucess(authenticate);
        }
    }
}
