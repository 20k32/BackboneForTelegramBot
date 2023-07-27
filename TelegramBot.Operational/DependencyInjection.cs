using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Operational.TelegramBotUpdateHandler;

namespace TelegramBot.Operational
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddServicesForOperationalLayer(this IServiceCollection services) =>
            services.AddSingleton<TelegramBotEventsHandler>()
            .AddSingleton<TelegramBotController>();
    }
}
