using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelegramBot.Core.Models.Configuration
{
    internal static class BotConfigurationExtensions
    {
        public static IServiceCollection ApplyConfigurationFromSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var botConfiguration = new BotConfiguration();
            var programConfiguration = new ProgramConfiguration();
            var programNotifications = new ProgramNotifications();

            configuration.Bind(nameof(BotConfiguration), botConfiguration);
            configuration.Bind(nameof(ProgramConfiguration), programConfiguration);
            configuration.Bind(nameof(ProgramNotifications), programNotifications);

            services.AddSingleton(botConfiguration);
            services.AddSingleton(programConfiguration);
            services.AddSingleton(programNotifications);

            return services;
        }
    }
}
