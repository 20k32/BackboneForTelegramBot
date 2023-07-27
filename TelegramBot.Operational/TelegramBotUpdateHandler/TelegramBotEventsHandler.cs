using System.ComponentModel.DataAnnotations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Events.Cache;
using TelegramBot.Core.Logging;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Operational.TelegramBotUpdateHandler
{
    internal sealed class TelegramBotEventsHandler
    {
        private ITelegramBotClient TelegramClient = null!;
        private ILogger Logger = null!;
        private EventCache Events = null!;
        private ProgramConfiguration Configuration = null!;
        private TextPlaceHolders Placeholders = null!;
        private BotInlineCommands InlineCommands = null!;

        public TelegramBotEventsHandler(ITelegramBotClient telegramBotClient,
            ProgramConfiguration programConfig,
            TextPlaceHolders textPlaceHolders,
            BotInlineCommands inlineCommands,
            ILogger logger,
            EventCache events) =>
            (TelegramClient, Configuration, Logger, Events, Placeholders, InlineCommands) =
                (telegramBotClient, programConfig, logger, events, textPlaceHolders, inlineCommands);

        private async Task ConditionToStopBot()
        {
            var message = string.Empty;
            while (message != Configuration.ConditionToStopProgram)
            {
                message = await Console.In.ReadLineAsync();

                if (message == Configuration.ConditionToClearConsole)
                {
                    Console.Clear();
                    await Logger.LogProgramStartAsync();
                }
            }
        }

        public async Task StartReceiving()
        {
            await Logger.LogProgramStartAsync();

            using (var cts = new CancellationTokenSource())
            {
                TelegramClient.StartReceiving(HandleUpdateAsync, HandleExceptionAsync, cancellationToken: cts.Token);

                await ConditionToStopBot();
                cts.Cancel();
            }

            await Logger.LogProgramEndAsync();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update is null)
            {
                await Task.CompletedTask;
            }

            switch (update!.Type)
            {
                case UpdateType.Message
                    when update.Message is { } message:
                        await HandleTextMessage(botClient, message, cancellationToken);
                    break;
                case UpdateType.CallbackQuery
                    when update.CallbackQuery is { } callbackQuery:
                        await HandleCallbackQuery(botClient, callbackQuery, cancellationToken);
                    break;
            }
        }

        private Task HandleTextMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var id = message.Chat.Id;
            var text = message.Text;
            var result = Task.CompletedTask;

            if (text is null)
            {
                return result;
            }

            if (text == Events.ComponentsCache.Commands.HideButtonsCommand)
            {
                result = Events.SendHideButtonsCommand(botClient, cancellationToken, id);
            }
            else if (text == Events.ComponentsCache.Commands.ShowButtonsCommand)
            {
                result = Events.SendShowButtonsCommand(botClient, cancellationToken, id);
            }
            else if (text == Events.ComponentsCache.Commands.ButtonPlaceHolder1)
            {
                result = Events.SendTextMessageCommand(botClient, cancellationToken, id, Placeholders.PlaceHolder1);
            }
            else if(text == Events.ComponentsCache.Commands.ButtonPlaceHolder2)
            {
                result = Events.SendTextMessageWithInlineButtons(botClient, cancellationToken, id, Placeholders.PlaceHolder2);
            }
            else
            {
                var reversed = message.Text.Reverse();
                var reversedString = string.Join("", reversed);
                result = Events.SendTextMessageCommand(botClient, cancellationToken, id, reversedString);
            }
            return result;
        }

        private Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            var result = Task.CompletedTask;

            if (callbackQuery.Message is null)
            {
                return result;
            }

            var id = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;
            var text = callbackQuery.Data;
            

            if (text is null)
            {
                return result;
            }

            if (text == InlineCommands.InlineButtonOkCallback)
            {
                result = Events.SendEditMessageTextAndInlieMarkupCommand(botClient, cancellationToken, id, Placeholders.PlaceHolder3, messageId);
            }
            else if (text == InlineCommands.InlineButtonCancelCallback)
            {
                result = Events.SendEditMessageTextCommand(botClient, cancellationToken, id, Placeholders.PlaceHolder4, messageId);
            }
            else if(text == InlineCommands.LateralInlineKeyboardButtonNiceCallback)
            {
                result = Events.SendEditMessageTextCommand(botClient, cancellationToken, id, Placeholders.PlaceHolder6, messageId);
            }
            return result;
        }

        private Task HandleExceptionAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken) =>
           Logger.LogInformationAsync(ex.Message);
    }
}
