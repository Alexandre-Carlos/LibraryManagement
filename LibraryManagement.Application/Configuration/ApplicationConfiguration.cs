using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryManagement.Application.Commands.Books.Insert;
using LibraryManagement.Application.Commands.Loans.Notify;
using LibraryManagement.Application.Services.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagement.Application.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers()
            .AddValidation()
            .AddServices()
            .AddSendingServices();

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

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<INotifyDelayService, NotifyDelayService>();
            return services;
        }

        public static IServiceCollection AddSendingServices(this IServiceCollection services)
        {
            //services.AddTransient<MailKitEmailService>();
            services.AddSingleton<INetMailEmailService, NetMailEmailService>();
            return services;
        }
    }
}
