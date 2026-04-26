using MyApp.Application.Interfaces.External;
using MyApp.Application.Models.External;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace MyApp.Infrastructure.Services.External
{
    public class EmailQueue : IEmailQueue
    {
        private readonly Channel<Email> _queue = Channel.CreateUnbounded<Email>();

        public async ValueTask QueueAsync(Email email)
        {
            await _queue.Writer.WriteAsync(email);
        }

        public async ValueTask<Email> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }


}
