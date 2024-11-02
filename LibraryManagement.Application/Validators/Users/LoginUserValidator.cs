using FluentValidation;
using LibraryManagement.Application.Queries.Users.Login;

namespace LibraryManagement.Application.Validators.Users
{
    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator() 
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage(UserErrorMessages.EmailEmpty)
                .MaximumLength(100).WithMessage(UserErrorMessages.EmailMaximumLength)
                .Matches(@"[a-z0-9!#$%&'*+\=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+\=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
                .WithMessage(UserErrorMessages.EmailNotStandard);
        }
    }
}
