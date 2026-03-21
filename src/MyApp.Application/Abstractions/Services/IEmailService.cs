using MyApp.Application.Abstractions.Models;

namespace MyApp.Application.Abstractions.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}