using FluentValidation.TestHelper;
using LibraryManagement.Application.Commands.Books.Update;
using LibraryManagement.Application.Validators.Books;

namespace LibraryManagement.Tests.Validators.Book
{
    public class UpdateBookValidatorTests
    {
        private readonly UpdateBookValidator _validator;

        public UpdateBookValidatorTests()
        {
            _validator = new UpdateBookValidator();
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Title()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Author = "John Doe", Isbn = "1234567890123", YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Title)
                  .WithErrorMessage(BookErrorMessages.TitleEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Title_Exceeding_Length()
        {
            // Arrange
            var longTitle = new string('a', 101);
            var command = new UpdateBookCommand { Id = 1, Title = longTitle, Author = "John Doe", Isbn = "1234567890123", YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Title)
                  .WithErrorMessage(BookErrorMessages.TitleMaximuLength);
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Author()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Isbn = "1234567890123", YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Author)
                  .WithErrorMessage(BookErrorMessages.AuthorEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Author_Exceeding_Length()
        {
            // Arrange
            var longAuthor = new string('a', 101);
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = longAuthor, Isbn = "1234567890123", YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Author)
                  .WithErrorMessage(BookErrorMessages.AuthorMaximuLength);
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Isbn()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Isbn)
                  .WithErrorMessage(BookErrorMessages.IsbnEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Isbn_Less_Than_Minimum_Length()
        {
            // Arrange
            var shortIsbn = "12345678901";
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", Isbn = shortIsbn, YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Isbn)
                  .WithErrorMessage(BookErrorMessages.IsbnMinimumLength);
        }

        [Fact]
        public void Should_Have_Error_For_Invalid_Isbn_Format()
        {
            // Arrange
            var invalidIsbn = "123456789012"; // Missing a digit
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", Isbn = invalidIsbn, YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Isbn)
                  .WithErrorMessage(BookErrorMessages.IsbnNotStandard);
        }

        [Fact]
        public void Should_Have_Error_For_Isbn_With_Invalid_Characters()
        {
            // Arrange
            var invalidIsbn = "1234567890123X"; // Contains a letter
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", Isbn = invalidIsbn, YearPublished = 2023 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.Isbn)
                  .WithErrorMessage(BookErrorMessages.IsbnNotStandard);
        }

        [Fact]
        public void Should_Have_Error_For_Year_Published_Less_Than_1700()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", Isbn = "1234567890123", YearPublished = 1699 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.YearPublished)
                  .WithErrorMessage(BookErrorMessages.YearPublishedGreaterOrEqual);
        }

        [Fact]
        public void Should_Have_Error_For_Year_Published_Greater_Than_Current_Year()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1, Title = "My New Book", Author = "John Doe", Isbn = "1234567890123", YearPublished = DateTime.Now.Year + 1 };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(book => book.YearPublished)
                  .WithErrorMessage(BookErrorMessages.YearPublishedLessOrEqual);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Command()
        {
            // Arrange
            var command = new UpdateBookCommand
            {
                Id = 1,
                Title = "My New Book",
                Author = "John Doe",
                Isbn = "1234567890123",
                YearPublished = 2023
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
