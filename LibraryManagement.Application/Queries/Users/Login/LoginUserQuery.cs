﻿using LibraryManagement.Application.Dtos;
using MediatR;

namespace LibraryManagement.Application.Queries.Users.Login
{
    public class LoginUserQuery : IRequest<ResultViewModel<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
