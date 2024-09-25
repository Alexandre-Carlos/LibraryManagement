using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.Application.Commands.Books.Insert;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers()
            .AddValidation();

            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services) 
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<InsertBookCommand>()
            );

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<InsertBookCommand>();

            return services;
        }
    }
}
