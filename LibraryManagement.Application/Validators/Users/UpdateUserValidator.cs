﻿using FluentValidation;
using LibraryManagement.Application.Commands.Users.Update;

namespace LibraryManagement.Application.Validators.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage(UserErrorMessages.NameEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.NameMaximumLength);

            RuleFor(u => u.Email).NotEmpty().WithMessage(UserErrorMessages.EmailEmpty)
                //.Matches(@"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$")
                .Matches(@"[a-z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
                .WithMessage(UserErrorMessages.EmailNotStandard);
        }
    }
}
