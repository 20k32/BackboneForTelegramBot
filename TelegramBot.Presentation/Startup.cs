using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Core;
using TelegramBot.Core.Models.Configuration;

namespace TelegramBot.Presentation
{
    internal sealed class Startup
    {
        private IConfiguration Configuration = null!;

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServicesForCoreLayer(Configuration);

            return services.BuildServiceProvider();
        }
    }
}
