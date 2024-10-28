using FluentValidation;
using LibraryManagement.Application.Queries.Users.Login;

namespace LibraryManagement.Application.Validators.Users
{
    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator() 
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(UserErrorMessages.EmailEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.EmailMaximuLength)
                .Matches(@"[a-z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
                .WithMessage(UserErrorMessages.EmailNotStandard);

            RuleFor(u => u.Password).NotEmpty().WithMessage(UserErrorMessages.NameEmpty)
                    .MaximumLength(20).WithMessage(UserErrorMessages.PasswordMaximuLength)
                    .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\1)){20,}$")
                    .WithMessage(UserErrorMessages.PasswordNotStandard);
        }
    }
}
