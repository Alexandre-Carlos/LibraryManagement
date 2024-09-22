using LibraryManagement.Application.Commands.Books.Insert;
using LibraryManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddService()
                    .AddHandlers();
            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services) 
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IUserService, UserService>();

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
