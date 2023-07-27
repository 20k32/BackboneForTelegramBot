using MediatR;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Events;

namespace TelegramBot.Core.Events.Cache
{
    internal sealed class EventCache
    {
        public GuiComponentsCache ComponentsCache = null!;
        private ButtonsCommand ButtonCommand = null!;
        private IMediator Mediator = null!;

        public EventCache(GuiComponentsCache componentsCache, IMediator mediator) =>
            (ComponentsCache, Mediator) = (componentsCache, mediator);

        public void EnsureButtosAreCreated(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            long chatId,
            string message,
            IReplyMarkup replyMarkup)
        {
            if (ButtonCommand == null)
            {
                ButtonCommand = new(
                    botClient,
                    cancellationToken,
                    replyMarkup,
                    message,
                    chatId);
            }
            else
            {
                ButtonCommand.ChatId = chatId;
                ButtonCommand.ReplyMarkup = replyMarkup;
                ButtonCommand.BotCommand = message;
            }
        }

        public Task<Message> SendShowButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            EnsureButtosAreCreated(
                botClient,
                cancellationToken,
                chatId,
                ComponentsCache.Notifications.ShowButtonsCommandNotification,
                ComponentsCache.HomeReplyMarkup);

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendHideButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            EnsureButtosAreCreated(
                botClient,
                cancellationToken,
                chatId,
                ComponentsCache.Notifications.HideButtonsCommandNotification,
                ComponentsCache.EmptyReplyMarkup);

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendTextMessageWithInlineButtons(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string message)
        {
            EnsureButtosAreCreated(
                botClient,
                cancellationToken,
                chatId,
                message,
                ComponentsCache.InlineKeyboardMarkup);

            return Mediator.Send(ButtonCommand);
        }

    }
}
