using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sot.Display.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sot.Display.HostedServices
{
    public class CountUpHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<CountUpHostedService> _logger;
        private readonly CountUpService _countUpService;
        private Timer _timer;

        public CountUpHostedService(
            ILogger<CountUpHostedService> logger,
            CountUpService countUpService)
        {
            _logger = logger;
            _countUpService = countUpService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Count start.");

            _timer = new Timer(IncreaseCountUp, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(0.5));

            return Task.CompletedTask;
        }

        private void IncreaseCountUp(object state)
        {
            _countUpService.ExecuteTickAsync().GetAwaiter().GetResult();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
