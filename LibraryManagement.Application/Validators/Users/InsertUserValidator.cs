using FluentValidation;
using LibraryManagement.Application.Commands.Users.Insert;

namespace LibraryManagement.Application.Validators.Users
{
    public class InsertUserValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage(UserErrorMessages.NameEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.NameMaximumLength);

            RuleFor(u => u.Email).NotEmpty().WithMessage(UserErrorMessages.EmailEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.EmailMaximumLength)
                .Matches(@"[a-z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
                .WithMessage(UserErrorMessages.EmailNotStandard);

            RuleFor(u => u.Password).NotEmpty().WithMessage(UserErrorMessages.PasswordEmpty)
                .MaximumLength(20).WithMessage(UserErrorMessages.NameMaximumLength)
                .MinimumLength(8).WithMessage(UserErrorMessages.PasswordMinimumLength)
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\1)){8,}$")
                .WithMessage(UserErrorMessages.PasswordNotStandard);
        }

    }
}
