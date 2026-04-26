using MyApp.Application.Interfaces.External;
using MyApp.Application.Models.External;

public class EmailService : IEmailService
{
    private readonly IEmailQueue _queue;

    public EmailService(IEmailQueue queue)
    {
        _queue = queue;
    }

    public async Task SendAsync(Email email)
    {
        await _queue.QueueAsync(email);
    }
}
