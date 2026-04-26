using MyApp.Application.Models.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.External
{
    public interface IEmailQueue
    {
        ValueTask QueueAsync(Email email);
        ValueTask<Email> DequeueAsync(CancellationToken cancellationToken);
    }
}
