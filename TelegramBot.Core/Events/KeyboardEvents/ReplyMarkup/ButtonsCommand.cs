using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Core.Events.KeyboardEvents
{
    internal class ButtonsCommand : ReplyMarkupEventBase, IRequest<Message>
    {
        public ButtonsCommand(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            ReplyMarkupBase replyMarkup,
            string command,
            long chatId) : base(botClient, cancellationToken, replyMarkup, command, chatId)
        { }
    }
}
