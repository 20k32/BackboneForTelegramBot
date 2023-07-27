using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Events.TextEvent
{
    internal class TextCommand : EventBase, IRequest<Message>
    {
        public TextCommand(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            string command,
            long chatId) : base(botClient, cancellationToken, command, chatId)
        { }
    }
}
