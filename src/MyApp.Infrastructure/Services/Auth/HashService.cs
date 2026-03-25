using MyApp.Application.Interfaces.Auth;
using System.Security.Cryptography;
using System.Text;

namespace MyApp.Infrastructure.Services.Auth
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
