using System.Security.Cryptography;

namespace MyApp.Domain.Extentions
{
    public static class SequentialGuid
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        public static Guid NewSequentialGuid()
        {
            // Lấy 10 byte ngẫu nhiên
            var randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            // Lấy timestamp hiện tại
            long timestamp = DateTime.UtcNow.Ticks / 10000L; // millisecond precision

            // Convert timestamp sang byte
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(timestampBytes);

            // Gộp 6 byte thời gian đầu + 10 byte random
            byte[] guidBytes = new byte[16];
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

            return new Guid(guidBytes);
        }
    }
}
