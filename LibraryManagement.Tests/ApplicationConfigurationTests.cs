using LibraryManagement.Application.Commands.Books.Insert;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibraryManagement.Application.Tests
{
    public class ApplicationConfigurationTests
    {
        [Fact]
        public void AddHandlers_Should_Register_MediatR_Handler()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddApplication();

            // Act
            var serviceProvider = services.BuildServiceProvider();
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // Assert
            Assert.NotNull(mediator);
        }
    }
}