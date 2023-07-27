using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Events.EditEvent
{
    internal class EditCommandHandler : IRequestHandler<EditCommand, Message>
    {
        public Task<Message> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var resultTask = request.BotClient.EditMessageTextAsync(
                request.ChatId,
                request.MessageId,
                request.BotCommand,
                cancellationToken: cancellationToken);
            
            if(request.ReplyMarkup is not null)
            {
                resultTask.ContinueWith(_ => 
                    request.BotClient.EditMessageReplyMarkupAsync(
                        request.ChatId,
                        request.MessageId,
                        replyMarkup: request.ReplyMarkup,
                        cancellationToken: cancellationToken));
            }

            return resultTask;
        }
    }
}
