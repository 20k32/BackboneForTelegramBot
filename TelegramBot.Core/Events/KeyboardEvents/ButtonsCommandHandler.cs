using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Events
{
    internal class ButtonsCommandHandler : IRequestHandler<ButtonsCommand, Message>
    {
        public Task<Message> Handle(ButtonsCommand request, CancellationToken cancellationToken)
        {
            return request.BotClient.SendTextMessageAsync(
                request.ChatId,
                text: request.BotCommand,
                replyMarkup: request.ReplyMarkup,
                cancellationToken: cancellationToken);
        }
    }
}
