using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Extentions
{
    public static class GenerateUnique
    {
        public static string GenerateUniqueSku(string prefix)
        {
            // Dùng mảng ký tự hoặc Random để đảm bảo tính duy nhất cao hơn Ticks đơn thuần
            var ticks = DateTimeOffset.UtcNow.Ticks.ToString();
            var uniquePart = ticks.Substring(ticks.Length - 10);

            // Thêm 2 ký tự ngẫu nhiên để triệt tiêu khả năng trùng trong cùng 1 tick
            var randomTag = Guid.NewGuid().ToString("N").Substring(0, 2).ToUpper();

            return $"{prefix.ToUpper()}-{uniquePart}{randomTag}";
        }
    }
}
