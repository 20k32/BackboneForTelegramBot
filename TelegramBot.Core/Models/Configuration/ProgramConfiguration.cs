namespace TelegramBot.Core.Models.Configuration
{
    internal sealed class ProgramConfiguration
    {
        public string ConditionToStopProgram { get; set; } = null!;
        public string ConditionToClearConsole { get; set; } = null!;

    }
}
