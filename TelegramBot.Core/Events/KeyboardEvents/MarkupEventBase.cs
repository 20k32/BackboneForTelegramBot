using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events
{
    internal class MarkupEventBase : EventBase
    {
        public IReplyMarkup ReplyMarkup = null!;

        public MarkupEventBase(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            IReplyMarkup replyMarkup,
            string command,
            long chatId) : base(botClient, cancellationToken, command, chatId) =>
        (ReplyMarkup) = (replyMarkup);
    }
}
