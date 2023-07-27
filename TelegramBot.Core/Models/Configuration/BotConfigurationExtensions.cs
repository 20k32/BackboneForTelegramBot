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
            var botCommands = new BotCommands();
            var botNotifications = new BotCommandsNotification();
            var botInlineCommands = new BotInlineCommands();
            var textPlaceHolders = new TextPlaceHolders();

            configuration.Bind(nameof(BotConfiguration), botConfiguration);
            configuration.Bind(nameof(ProgramConfiguration), programConfiguration);
            configuration.Bind(nameof(ProgramNotifications), programNotifications);
            configuration.Bind(nameof(BotCommands), botCommands);
            configuration.Bind(nameof(BotCommandsNotification), botNotifications);
            configuration.Bind(nameof(BotInlineCommands), botInlineCommands);
            configuration.Bind(nameof(TextPlaceHolders), textPlaceHolders);

            services.AddSingleton(botConfiguration);
            services.AddSingleton(programConfiguration);
            services.AddSingleton(programNotifications);
            services.AddSingleton(botCommands);
            services.AddSingleton(botNotifications);
            services.AddSingleton(botInlineCommands);
            services.AddSingleton(textPlaceHolders);

            return services;
        }
    }
}
