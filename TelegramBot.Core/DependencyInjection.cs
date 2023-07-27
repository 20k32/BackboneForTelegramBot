using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Core.Events;
using TelegramBot.Core.Events.Cache;
using TelegramBot.Core.Logging;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Core
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddServicesForCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplyConfigurationFromSettings(configuration);
            services.AddTelegramBotEvents();
            services.RegisterCustomLogger();

            var provider = services.BuildServiceProvider();

            var token = provider.GetService<BotConfiguration>()!.BotKey;
            var client = new TelegramBotClient(token);

            var commands = provider.GetService<GuiComponentsCache>();
            client.SetMyCommandsAsync(commands!.TelegramBotCommands);

            services.AddSingleton<ITelegramBotClient>(client);

            return services;
        }
    }
}
