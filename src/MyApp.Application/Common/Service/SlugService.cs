using MyApp.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.Application.Common.Service
{
    public class SlugService : ISlugService
    {
        public string Generate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // bỏ dấu tiếng Việt
            string normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                var unicode = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicode != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            string result = sb.ToString().Normalize(NormalizationForm.FormC);

            // lowercase
            result = result.ToLowerInvariant();

            // bỏ ký tự đặc biệt
            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");

            // thay khoảng trắng thành "-"
            result = Regex.Replace(result, @"\s+", "-");

            // bỏ nhiều "-"
            result = Regex.Replace(result, @"-+", "-");

            // trim "-"
            result = result.Trim('-');

            return result;
        }
    }
}
