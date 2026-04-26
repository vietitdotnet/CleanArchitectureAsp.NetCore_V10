using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyApp.Application.Interfaces.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Services.External
{
    public class EmailBackgroundWorker : BackgroundService
    {
        private readonly IEmailQueue _queue;
        private readonly EmailSender _sender;
        private readonly ILogger<EmailBackgroundWorker> _logger;

        public EmailBackgroundWorker(
            IEmailQueue queue,
            EmailSender sender,
            ILogger<EmailBackgroundWorker> logger)
        {
            _queue = queue;
            _sender = sender;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var email = await _queue.DequeueAsync(stoppingToken);

                    await RetryAsync(() => _sender.SendAsync(email));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Send email failed");
                }
            }
        }

        private async Task RetryAsync(Func<Task> action, int retry = 3)
        {
            for (int i = 0; i < retry; i++)
            {
                try
                {
                    await action();
                    return;
                }
                catch
                {
                    if (i == retry - 1) throw;
                    await Task.Delay(1000 * (i + 1));
                }
            }
        }
    }

}
