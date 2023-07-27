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
        public readonly IEnumerable<BotCommand> TelegramBotCommands = null!;

        public GuiComponentsCache(BotCommands commands)
        {
            Commands = commands;

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
                new KeyboardButton[] { new(Commands.HideButtonsButtonName) }
            });
        }
    }
}
