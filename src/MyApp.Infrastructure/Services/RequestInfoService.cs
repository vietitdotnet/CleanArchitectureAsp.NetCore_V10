using Microsoft.AspNetCore.Http;
using MyApp.Application.Abstractions.Services;
using UAParser;

namespace MyApp.Infrastructure.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserAgent()
        {
            return _httpContextAccessor.HttpContext?
                .Request.Headers["User-Agent"].ToString();
        }

        public string? GetIpAddress()
        {
            var context = _httpContextAccessor.HttpContext;

            var ip = context?.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(ip))
                return ip.Split(',').First();

            return context?.Connection.RemoteIpAddress?.ToString();
        }

        public string? GetDevice()
        {
            var userAgent = GetUserAgent();

            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            var parser = Parser.GetDefault();
            var client = parser.Parse(userAgent);

            var device = client.Device.Family;
            var os = client.OS.Family;
            var browser = client.UA.Family;

            return $"{device} - {os} - {browser}";
        }
    }


}
