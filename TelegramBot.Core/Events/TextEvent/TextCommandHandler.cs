using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Events.TextEvent
{
    internal class TextCommandHandler : IRequestHandler<TextCommand, Message>
    {
        public Task<Message> Handle(TextCommand request, CancellationToken cancellationToken)
        {
            return request.BotClient.SendTextMessageAsync(
                request.ChatId,
                request.BotCommand,
                cancellationToken: cancellationToken);
        }
    }
}
