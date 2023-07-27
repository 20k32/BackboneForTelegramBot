using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events.KeyboardEvents
{
    internal class ReplyMarkupEventBase : EventBase
    {
        public ReplyMarkupBase ReplyMarkup = null!;

        public ReplyMarkupEventBase(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            ReplyMarkupBase replyMarkup,
            string command,
            long chatId) : base(botClient, cancellationToken, command, chatId) =>
        (ReplyMarkup) = (replyMarkup);
    }
}
