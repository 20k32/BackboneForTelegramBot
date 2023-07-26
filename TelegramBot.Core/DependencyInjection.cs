﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Core.Logging;
using TelegramBot.Core.Models.Configuration;
using TelegramBot.Core.TelegramBotUpdateHandler;

namespace TelegramBot.Core
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddServicesForCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplyConfigurationFromSettings(configuration);
            services.RegisterCustomLogger();
            
            var token = services.BuildServiceProvider().GetService<BotConfiguration>()!.BotKey;
            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(token));

            services.AddSingleton<TelegramBotEventsHandler>();
            services.AddSingleton<TelegramBotController>();

            return services;
        }
            
    }
}
