using MediatR;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core.Events.KeyboardEvents;

namespace TelegramBot.Core.Events.Cache
{
    internal sealed class EventCache
    {
        public GuiComponentsCache ComponentsCache = null!;
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
                ButtonCommand.ReplyMarkup = ComponentsCache.HomeReplyMarkup;
                ButtonCommand.BotCommand = ComponentsCache.Commands.ShowButtonsCommand;
            }

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendHideButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            if (ButtonCommand == null)
            {
                ButtonCommand = new(
                    botClient,
                    cancellationToken,
                    ComponentsCache.EmptyReplyMarkup,
                    ComponentsCache.Commands.HideButtonsCommand,
                    chatId);
            }
            else
            {
                ButtonCommand.ChatId = chatId;
                ButtonCommand.ReplyMarkup = ComponentsCache.EmptyReplyMarkup;
                ButtonCommand.BotCommand = ComponentsCache.Commands.HideButtonsCommand;
            }

            return Mediator.Send(ButtonCommand);
        }
    }
}
