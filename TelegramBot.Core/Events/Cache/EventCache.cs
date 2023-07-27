using MediatR;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Events;
using TelegramBot.Core.Events.EditEvent;
using TelegramBot.Core.Events.TextEvent;

namespace TelegramBot.Core.Events.Cache
{
    internal sealed class EventCache
    {
        public GuiComponentsCache ComponentsCache = null!;
        private IMediator Mediator = null!;

        private ButtonsCommand ButtonCommand = null!;
        private TextCommand TextCommand;
        private EditCommand EditCommand;

        public EventCache(GuiComponentsCache componentsCache, IMediator mediator) =>
            (ComponentsCache, Mediator) = (componentsCache, mediator);

        public void EnsureButtonsAreCreated(
            ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            long chatId,
            string message,
            IReplyMarkup replyMarkup)
        {
            if (ButtonCommand is null)
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

        public void EnsureEditCommandIsCreated(ITelegramBotClient botClient,
            CancellationToken cancellationToken,
            long chatId,
            string message,
            int messageId,
            InlineKeyboardMarkup replyMarkup)
        {
            if (EditCommand is null)
            {
                EditCommand = new(
                    botClient,
                    cancellationToken,
                    message,
                    messageId,
                    chatId,
                    replyMarkup);
            }
            else
            {
                EditCommand.ChatId = chatId;
                EditCommand.ReplyMarkup = replyMarkup;
                EditCommand.BotCommand = message;
                EditCommand.MessageId = messageId;
            }
        }

        public Task<Message> SendShowButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            EnsureButtonsAreCreated(
                botClient,
                cancellationToken,
                chatId,
                ComponentsCache.Notifications.ShowButtonsCommandNotification,
                ComponentsCache.HomeReplyMarkup);

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendHideButtonsCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
        {
            EnsureButtonsAreCreated(
                botClient,
                cancellationToken,
                chatId,
                ComponentsCache.Notifications.HideButtonsCommandNotification,
                ComponentsCache.EmptyReplyMarkup);

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendTextMessageWithInlineButtons(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string message)
        {
            EnsureButtonsAreCreated(
                botClient,
                cancellationToken,
                chatId,
                message,
                ComponentsCache.InlineKeyboardMarkup);

            return Mediator.Send(ButtonCommand);
        }

        public Task<Message> SendTextMessageCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string message)
        {
            if(TextCommand is null)
            {
                TextCommand = new(botClient, cancellationToken, message, chatId);
            }
            else
            {
                TextCommand.ChatId = chatId;
                TextCommand.BotCommand = message;
            }

            return Mediator.Send(TextCommand);
        }

        public Task<Message> SendEditMessageTextCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string message, int messageId)
        {
            EnsureEditCommandIsCreated(botClient, cancellationToken, chatId, message, messageId, null!);

            return Mediator.Send(EditCommand);
        }

        public Task<Message> SendEditMessageTextAndInlieMarkupCommand(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string message, int messageId)
        {
            EnsureEditCommandIsCreated(botClient, cancellationToken, chatId, message, messageId, ComponentsCache.LateralInlineKeyboardMarkup);

            return Mediator.Send(EditCommand);
        }
    }
}
