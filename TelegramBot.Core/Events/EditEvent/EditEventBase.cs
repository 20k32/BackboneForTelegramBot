using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events.EditEvent
{
    internal class EditEventBase : EventBase
    {
        public int MessageId { get; set; }
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        public EditEventBase(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            string command,
            int messageId,
            long chatId,
            InlineKeyboardMarkup replyMarkup) : base(botClient, cancellationToken, command, chatId)
        {
            MessageId = messageId;
            ReplyMarkup = replyMarkup;
        }
    }
}
