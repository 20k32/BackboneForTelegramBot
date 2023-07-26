using Microsoft.Extensions.DependencyInjection;

namespace TelegramBot.Core.Logging
{
    internal static class DependencyInjection
    {
        public static IServiceCollection RegisterCustomLogger(this IServiceCollection services) =>
            services.AddSingleton<ILogger, CustomLogger>();
    }
}
