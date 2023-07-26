namespace TelegramBot.Core.Logging
{
    internal interface ILogger
    {
        Task LogInformationAsync(string message);
        Task LogProgramStartAsync();
        Task LogProgramEndAsync();
    }
}
