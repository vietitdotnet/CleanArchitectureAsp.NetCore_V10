namespace MyApp.Application.Models.External
{
    public class Email
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;
        public List<MailAttachment>? Attachments { get; set; }
    }
}