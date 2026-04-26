using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MyApp.Application.Models.External;


namespace MyApp.Infrastructure.Services.External
{
  
    public class EmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(Email email)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(email.From));
            AddAddresses(message.To, email.To);
            AddAddresses(message.Cc, email.Cc);
            AddAddresses(message.Bcc, email.Bcc);

            message.Subject = email.Subject;

            message.Body = new TextPart("html")
            {
                Text = email.Body
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _config["Smtp:Host"]!,
                int.Parse(_config["Smtp:Port"]!),
                SecureSocketOptions.StartTls
            );

            await client.AuthenticateAsync(
                _config["Smtp:UserName"]!,
                _config["Smtp:Password"]!
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        private void AddAddresses(InternetAddressList list, string? addresses)
        {
            if (string.IsNullOrWhiteSpace(addresses)) return;

            var arr = addresses
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim());

            foreach (var addr in arr)
            {
                list.Add(MailboxAddress.Parse(addr));
            }
        }
    }

}
