using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Operational.TelegramBotUpdateHandler;

namespace TelegramBot.Presentation
{
    internal sealed class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("AppSettings.json")
                    .Build();

            var services = new ServiceCollection();
            
            var startup = new Startup(configuration);
            
            var provider = startup.ConfigureServices(services);

            var controller = provider.GetService<TelegramBotController>();

            await controller!.StartBot();
        }
    }
}