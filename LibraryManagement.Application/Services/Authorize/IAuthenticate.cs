using LibraryManagement.Application.Services.Authorize;

namespace LibraryManagement.Core.Account
{
    public interface IAuthenticate
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<bool> UserExist(string email);
        Task<HashResponse> GenerateHashPassword(string email, string password);

        Task<HashResponse> CreateHashPassword(string email, string password);
    }
}
