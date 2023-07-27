using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
        public ProgramConfiguration Configuration = null!;


        public TelegramBotEventsHandler(ITelegramBotClient telegramBotClient,
            ProgramConfiguration programConfig,
            ILogger logger,
            EventCache events) =>
            (TelegramClient, Configuration, Logger, Events) =
                (telegramBotClient, programConfig, logger, events);

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
                case UpdateType.Message when update.Message is not null:
                    await Events.SendShowButtonsCommand(botClient, cancellationToken, update.Message.Chat.Id);
                    break;
                case UpdateType.CallbackQuery: break;
            }
        }

        private Task HandleExceptionAsync(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken) =>
           Logger.LogInformationAsync(ex.Message);
    }
}
