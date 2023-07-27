using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Core.Events
{
    internal class EventBase
    {
        public ITelegramBotClient BotClient = null!;
        public CancellationToken CancellationToken;
        public string BotCommand = null!;
        public long ChatId;

        public EventBase(ITelegramBotClient botClient,
            CancellationToken
            cancellationToken,
            string command,
            long chatId) =>
            (BotClient, CancellationToken, BotCommand, ChatId) =
                (botClient, cancellationToken, command, chatId);
    }
}
