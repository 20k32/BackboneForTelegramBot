namespace TelegramBot.Core.Models.Configuration
{
    internal sealed class ProgramNotifications
    {
        public string ProgramStarted { get; set; } = null!;
        public string ProgramCompleted { get; set; } = null!;
        public string ClearWindow { get; set; } = null!;
    }
}
