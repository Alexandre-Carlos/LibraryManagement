﻿using FluentValidation;
using LibraryManagement.Application.Commands.Users.Insert;

namespace LibraryManagement.Application.Validators.Users
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage(UserErrorMessages.NameEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.NameMaximuLength);

            RuleFor(u => u.Email).NotEmpty().WithMessage(UserErrorMessages.EmailEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.EmailMaximuLength)
                .Matches(@"[a-z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
                .WithMessage(UserErrorMessages.EmailNotStandard);
        }

    }
}
