using Telegram.Bot.Types;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Core.Logging
{
    internal sealed class ConsoleLogger : ILogger
    {
        private ProgramNotifications Notifications = null!;

        public ConsoleLogger(ProgramNotifications notifications) =>
            Notifications = notifications;

        public Task LogInformationAsync(string message) =>
            Console.Out.WriteLineAsync(message);

        public Task LogProgramStartAsync() =>
            Console.Out.WriteLineAsync(Notifications.ProgramStarted +
                "\n" + Notifications.ClearWindow);

        public Task LogProgramEndAsync() =>
            Console.Out.WriteLineAsync(Notifications.ProgramCompleted);
    }
}
