using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Core.Events.Cache
{
    internal sealed class GuiComponentsCache
    {
        public readonly ReplyKeyboardRemove EmptyReplyMarkup = null!;
        public readonly ReplyKeyboardMarkup HomeReplyMarkup = null!;
        public readonly BotCommands Commands = null!;
        public readonly BotCommandsNotification Notifications = null!;
        public readonly BotInlineCommands InlineCommands = null!;
        public readonly IEnumerable<BotCommand> TelegramBotCommands = null!;
        public readonly InlineKeyboardMarkup InlineKeyboardMarkup = null!;
        public readonly InlineKeyboardMarkup LateralInlineKeyboardMarkup = null!;

        public GuiComponentsCache(BotCommands commands, BotCommandsNotification notifications, BotInlineCommands inlineCommands)
        {
            Commands = commands;
            Notifications = notifications;
            InlineCommands = inlineCommands;

            TelegramBotCommands = new BotCommand[]
            {
                new()
                {
                    Command = Commands.HideButtonsCommand,
                    Description = Commands.HideButtonsCommandDescription,
                },
                new()
                {
                    Command = commands.ShowButtonsCommand,
                    Description = Commands.ShowButtonsCommandDescription,
                }
            };

            EmptyReplyMarkup = new();

            HomeReplyMarkup = new(new KeyboardButton[][]
            {

                new KeyboardButton[] { new(Commands.ButtonPlaceHolder1) },
                new KeyboardButton[] { new(Commands.ButtonPlaceHolder2) }
            });

            InlineKeyboardMarkup = new(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(InlineCommands.InlineButtonOkName, InlineCommands.InlineButtonOkCallback),
                    InlineKeyboardButton.WithCallbackData(InlineCommands.InlineButtonCancelName, InlineCommands.InlineButtonCancelCallback)
                }
            });

            LateralInlineKeyboardMarkup = new(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(InlineCommands.LateralInlineKeyboardButtonNiceName,
                        InlineCommands.LateralInlineKeyboardButtonNiceCallback),
                    InlineKeyboardButton.WithCallbackData(InlineCommands.InlineButtonCancelName,
                        InlineCommands.InlineButtonCancelCallback)
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl(InlineCommands.LateralInlineKeyboardButtonGoToGoogleName,
                        "https://www.google.com")
                }
            });
        }
    }
}
