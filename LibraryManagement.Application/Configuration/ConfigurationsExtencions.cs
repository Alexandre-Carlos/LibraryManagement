using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Application.Configuration
{
    public static class ConfigurationsExtencions
    {
        public static ApplicationConfig LoadConfigurations(this IConfiguration configuration) => configuration.Get<ApplicationConfig>()!;
    }
}
