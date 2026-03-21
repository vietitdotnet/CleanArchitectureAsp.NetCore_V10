using MyApp.Application.Abstractions.Services;
using System.Security.Cryptography;
using System.Text;

namespace MyApp.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string GetHash(string input)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
