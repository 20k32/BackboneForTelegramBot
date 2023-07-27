using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.Events.KeyboardEvents;

namespace TelegramBot.Core.Events.Cache
{
    internal sealed class EventCache
    {
        private GuiComponentsCache ComponentsCache = null!;
        private ButtonsCommand ButtonCommand = null!;
        private IMediator Mediator = null!;

        public EventCache(GuiComponentsCache componentsCache, IMediator mediator) =>
            (ComponentsCache, Mediator) = (componentsCache, mediator);

        public Task<Message> SendShowButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            if (ButtonCommand == null)
            {
                ButtonCommand = new(
                    botClient,
                    cancellationToken,
                    ComponentsCache.HomeReplyMarkup,
                    ComponentsCache.Commands.ShowButtonsCommand,
                    chatId);
            }
            else
            {
                ButtonCommand.ChatId = chatId;
            }

            return Mediator.Send(ButtonCommand);
        }
    }
}
