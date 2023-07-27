namespace TelegramBot.Core.Models.Configuration
{
    internal class BotInlineCommands
    {
        public string InlineButtonOkName { get; init; } = null!;
        public string InlineButtonCancelName { get; init; } = null!;
        public string InlineButtonOkCallback { get; init; } = null!;
        public string InlineButtonCancelCallback { get; init; } = null!;
        public string LateralInlineKeyboardButtonNiceName { get; init; } = null!; 
        public string LateralInlineKeyboardButtonNiceCallback { get; init; } = null!;
        public string LateralInlineKeyboardButtonGoToGoogleName { get; init; } = null!;
    }
}
