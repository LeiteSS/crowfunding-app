using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using Vaquinha.App.Config;
using Vaquinha.App.Service.Interfaces;

namespace Vaquinha.App.Service 
{
    public class PollyService : IPollyService
    {
        private GlobalAppConfig _globalAppConfig;
        private readonly ILogger<PollyService> _logger;

        public PollyService(ILogger<PollyService> logger, GlobalAppConfig globalAppConfig)
        {
            _logger = logger;
            _globalAppConfig = globalAppConfig;
        }

        public AsyncRetryPolicy CreatePoliticWaitAndRetryFor(string method)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(_globalAppConfig.Polly.QuantityRetry,
                attempt => TimeSpan.FromSeconds(_globalAppConfig.Polly.WaitingInSegs),
                (exception, calculatedWaitDuration) =>
                {
                    _logger.LogError($"Erro ao acionar o metodo {method}. Total de tempo de retry até o momento: {calculatedWaitDuration}s");
                });

            return policy;
        }
    }
}