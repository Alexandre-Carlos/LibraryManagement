using FluentValidation.TestHelper;
using LibraryManagement.Application.Commands.Users.Insert;
using LibraryManagement.Application.Validators.Users;

namespace LibraryManagement.Tests.Validators.Users
{
    public class InsertUserValidatorTests
    {
        private readonly InsertUserValidator _validator;

        public InsertUserValidatorTests()
        {
            _validator = new InsertUserValidator();
        }


        [Theory]
        [InlineData("valid@email.com", false)]
        [InlineData("valid@.com", true)]
        [InlineData("valid@email.com.br", false)]
        [InlineData("valid@nãotenho.com", true)]
        [InlineData("teste@email", true)]
        public void Should_Have_Error_For_Incorrect_Or_Correct_Email(string email, bool erro)
        {
            // Arrange
            var command = new InsertUserCommand { Name = "teste", Email = email };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            if (erro)
                result.ShouldHaveValidationErrorFor(user => user.Email)
              .WithErrorMessage(UserErrorMessages.EmailNotStandard);
            else
                result.ShouldNotHaveAnyValidationErrors();

        }


        [Fact]
        public void Should_Have_Error_For_Empty_Name()
        {
            // Arrange
            var command = new InsertUserCommand { Email = "valid@email.com" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Name).WithErrorMessage(UserErrorMessages.NameEmpty);
        }

        [Fact]
        public void Should_Have_Error_For_Name_Exceeding_Length()
        {
            // Arrange
            var longName = new string('a', 101);
            var command = new InsertUserCommand { Name = longName, Email = "valid@email.com" };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Name)
                  .WithErrorMessage(UserErrorMessages.NameMaximuLength);
        }

        [Fact]
        public void Should_Have_Error_For_Empty_Email()
        {
            // Arrange
            var command = new InsertUserCommand { Name = "John Doe" };

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
            var invalidEmail = "invalid_email";
            var command = new InsertUserCommand { Name = "John Doe", Email = invalidEmail };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Email)
                  .WithErrorMessage(UserErrorMessages.EmailNotStandard);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Command()
        {
            // Arrange
            var validCommand = new InsertUserCommand
            {
                Name = "John Doe",
                Email = "valid@email.com"
            };

            // Act
            var result = _validator.TestValidate(validCommand);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
