﻿using LibraryManagement.Application.Configuration;
using LibraryManagement.Core.Account;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Repositories;

namespace LibraryManagement.Application.Services.Authorize
{

    public class AuthenticateService : IAuthenticate
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationConfig _appConfig;

        public AuthenticateService(IUserRepository userRepository, ApplicationConfig appConfig)
        {
            _userRepository = userRepository;
            _appConfig = appConfig;
        }

        public async Task<HashResponse> GenerateHashPassword(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user is null)
                return null;

            var hashLogin = SecurePasswordHasher.Hash(password);

            return hashLogin;
        }

        public async Task<HashResponse> CreateHashPassword(string email, string password)
        {
           var hashLogin = SecurePasswordHasher.Hash(password);

            return hashLogin;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            /* var hashLogin = SecurePasswordHasher.Hash(password, user.Salt);

             if (!hashLogin.Hash.Equals(user.Password))
                 return null;*/

            var teste = SecurePasswordHasher.Verify(password, user.Password);

            if (!teste)
                return null;

           var token = GenerateToken(user);

            return token;
        }

        public async Task<bool> UserExist(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null) return false;

            return true;
        }

        private string GenerateToken(User user)
        {
            var token = AuthenticationConfig.GenerateToken(user, _appConfig);

            return token;
        }
    }
}
