namespace TelegramBot.Core.Models.Configuration
{
    internal sealed class BotCommands
    {
        public string HideButtonsCommand { get; init; } = null!;
        public string HideButtonsCommandDescription { get; init; } = null!;
        public string HideButtonsButtonName { get; init; } = null!;
        public string ShowButtonsCommand { get; init; } = null!;
        public string ShowButtonsCommandDescription { get; init; } = null!;
    }
}
