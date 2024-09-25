using FluentValidation;
using LibraryManagement.Application.Commands.Books.Update;

namespace LibraryManagement.Application.Validators.Books
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(BookErrorMessages.TitleEmpty)
                .MaximumLength(100).WithMessage(BookErrorMessages.TitleMaximuLength);

            RuleFor(a => a.Author)
                .NotEmpty().WithMessage(BookErrorMessages.AuthorEmpty)
                .MaximumLength(100).WithMessage(BookErrorMessages.AuthorMaximuLength);

            RuleFor(i => i.Isbn)
                .NotEmpty().WithMessage(BookErrorMessages.IsbnEmpty)
                .MinimumLength(13).WithMessage(BookErrorMessages.IsbnMinimumLength)
                .Matches(@"^^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$").WithMessage(BookErrorMessages.IsbnNotStandard);

            RuleFor(y => y.YearPublished)
                .NotEmpty().WithMessage(BookErrorMessages.YearPublishedEmpty)
                .GreaterThanOrEqualTo(1700).WithMessage(BookErrorMessages.YearPublishedGreaterOrEqual)
                .LessThanOrEqualTo((DateTime.Now.Year)).WithMessage(BookErrorMessages.YearPublishedLessOrEqual);
        }
    }
}
