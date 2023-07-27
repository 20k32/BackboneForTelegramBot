using Microsoft.Extensions.DependencyInjection;

namespace TelegramBot.Core.Logging
{
    internal static class LoggingExtensions
    {
        public static IServiceCollection RegisterCustomLogger(this IServiceCollection services) =>
            services.AddSingleton<ILogger, ConsoleLogger>();
    }
}
