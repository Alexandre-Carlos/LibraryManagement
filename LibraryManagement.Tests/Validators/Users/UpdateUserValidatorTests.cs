using FluentValidation.TestHelper;
using LibraryManagement.Application.Commands.Users.Update;
using LibraryManagement.Application.Validators.Users;

namespace LibraryManagement.Tests.Validators.Users
{
    public class UpdateUserValidatorTests
    {

        private readonly UpdateUserValidator _validator;

        public UpdateUserValidatorTests()
        {
            _validator = new UpdateUserValidator();
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Name()
        {
            // Arrange
            var command = new UpdateUserCommand { Id = 1, Email = "user@example.com" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Name)
                    .WithErrorMessage(UserErrorMessages.NameEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Name_Exceeding_Length()
        {
            // Arrange
            var longName = new string('a', 101);
            var command = new UpdateUserCommand { Id = 1, Email = "user@example.com", Name = longName };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Name)
                    .WithErrorMessage(UserErrorMessages.NameMaximumLength);
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Email()
        {
            // Arrange
            var command = new UpdateUserCommand { Id = 1, Name = "John Doe" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Email)
                    .WithErrorMessage(UserErrorMessages.EmailEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Invalid_Email_Format()
        {
            // Arrange
            var invalidEmail1 = "invalid_email";
            var invalidEmail2 = "user@example"; // Missing domain extension

            // Act
            var result1 = _validator.TestValidate(new UpdateUserCommand { Id = 1, Name = "John Doe", Email = invalidEmail1 });
            var result2 = _validator.TestValidate(new UpdateUserCommand { Id = 1, Name = "John Doe", Email = invalidEmail2 });

            // Assert
            result1.ShouldHaveValidationErrorFor(user => user.Email)
                    .WithErrorMessage(UserErrorMessages.EmailNotStandard);
            result2.ShouldHaveValidationErrorFor(user => user.Email)
                    .WithErrorMessage(UserErrorMessages.EmailNotStandard);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Command()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                Id = 1,
                Name = "John Doe",
                Email = "user@example.com"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

