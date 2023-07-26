namespace TelegramBot.Core.TelegramBotUpdateHandler
{
    internal class TelegramBotController
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
