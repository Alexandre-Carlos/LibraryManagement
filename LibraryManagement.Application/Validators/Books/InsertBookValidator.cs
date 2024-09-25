using FluentValidation;
using LibraryManagement.Application.Commands.Books.Insert;
using System.Text.RegularExpressions;

namespace LibraryManagement.Application.Validators.Books
{
    public class InsertBookValidator : AbstractValidator<InsertBookCommand>
    {
        public InsertBookValidator()
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
              //.Must(BeIsbnRegex).WithMessage("Isbn deve conter apenas números ou hífens!");

            RuleFor(y => y.YearPublished)
                .NotEmpty().WithMessage(BookErrorMessages.YearPublishedEmpty)
                .GreaterThanOrEqualTo(1700).WithMessage(BookErrorMessages.YearPublishedGreaterOrEqual)
                .LessThanOrEqualTo((DateTime.Now.Year)).WithMessage(BookErrorMessages.YearPublishedLessOrEqual);

            RuleFor(q => q.Quantity)
                .NotEmpty().WithMessage(BookErrorMessages.QuantityEmpty)
                .GreaterThanOrEqualTo(1).WithMessage(BookErrorMessages.QuantityGreaterOrEqual)
                .LessThanOrEqualTo(30).WithMessage(BookErrorMessages.QuantityLessOrEqual);
        }
            private bool BeIsbnRegex(string isbn)
            {
                Regex regex = new Regex(@"^^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$", RegexOptions.IgnoreCase);

                if (!regex.IsMatch(isbn))
                {
                    return false;
                }
                return true;
            }
    }
}
