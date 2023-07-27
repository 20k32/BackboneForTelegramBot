using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events
{
    internal class ButtonsCommand : MarkupEventBase, IRequest<Message>
    {
        public ButtonsCommand(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            IReplyMarkup replyMarkup,
            string command,
            long chatId) : base(botClient, cancellationToken, replyMarkup, command, chatId)
        { }
    }
}
