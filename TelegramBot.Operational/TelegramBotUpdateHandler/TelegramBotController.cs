namespace TelegramBot.Operational.TelegramBotUpdateHandler
{
    internal sealed class TelegramBotController
    {
        private TelegramBotEventsHandler BotEventsHandler = null!;

        public TelegramBotController(TelegramBotEventsHandler telegramBotEventsHandler)
        {
            BotEventsHandler = telegramBotEventsHandler;
        }

        public Task StartBot() =>
            BotEventsHandler.StartReceiving();
       
    }
}
