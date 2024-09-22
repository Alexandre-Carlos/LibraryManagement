using LibraryManagement.Application.Commands.Books.Insert;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
            return services;
        }

        public static IServiceCollection AddHandlers(this IServiceCollection services) 
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<InsertBookCommand>()
            );

            return services;
        }
    }
}
