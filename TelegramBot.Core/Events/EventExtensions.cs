using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TelegramBot.Core.Events.Cache;

namespace TelegramBot.Core.Events
{
    internal static class EventExtensions
    {
        public static IServiceCollection AddTelegramBotEvents(this IServiceCollection services) =>
         services
            .AddSingleton<GuiComponentsCache>()
            .AddMediatR(configuration => 
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddSingleton<EventCache>();
    }
}
