using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Operational;

using TelegramBot.Core;

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
            services.AddServicesForOperationalLayer();

            return services.BuildServiceProvider();
        }
    }
}
