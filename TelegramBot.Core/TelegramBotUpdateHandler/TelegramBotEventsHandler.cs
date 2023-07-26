using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Core.Logging;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Core.TelegramBotUpdateHandler
{
    internal sealed class TelegramBotEventsHandler
    {
        private ITelegramBotClient TelegramClient = null!;
        private ILogger Logger = null!;
        public ProgramConfiguration Configuration = null!;

        public TelegramBotEventsHandler(ITelegramBotClient telegramBotClient,
            ProgramConfiguration programConfig,
            ILogger logger) =>
            (TelegramClient, Configuration, Logger) = (telegramBotClient, programConfig, logger);

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

            switch (update.Type)
            {
                case UpdateType.Message: break;
                case UpdateType.CallbackQuery: break;
            }

            await botClient.SendTextMessageAsync(update.Message.Chat.Id, update.Message.Text, cancellationToken: cancellationToken);
        }

        private Task HandleExceptionAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken) =>
           Logger.LogInformationAsync(ex.Message);
    }
}
