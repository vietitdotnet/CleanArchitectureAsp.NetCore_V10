using MyApp.Application.Models.External;

namespace MyApp.Application.Interfaces.External
{
    public interface IEmailService
    {
        Task SendAsync(Email email);
    }
}