using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events.EditEvent
{
    internal class EditCommand : EditEventBase, IRequest<Message>
    {
        public EditCommand(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            string command,
            int messageId,
            long chatId,
            InlineKeyboardMarkup replyMarkup) : base(botClient, cancellationToken, command, messageId, chatId, replyMarkup)
        { }
    }
}
