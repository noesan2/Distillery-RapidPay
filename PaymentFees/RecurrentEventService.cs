using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFees
{
    public class RecurrentEventService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<RecurrentEventService> _logger;
        private Timer? _timer = null;
        private IUfeService _ufeService;

        public RecurrentEventService(ILogger<RecurrentEventService> logger
            , IUfeService ufeService)
        { 
            _logger = logger;
            _ufeService = ufeService;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object? state)
        {
            // atomic operation
            var newValue = _ufeService.UpdateFeeDecimal();

            _logger.LogInformation(
                "Timed Hosted Service is working. UFE Decimal: {Count}", newValue);
        }
    }
}
