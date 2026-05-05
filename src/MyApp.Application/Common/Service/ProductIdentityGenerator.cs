using AutoMapper;
using MyApp.Application.Interfaces.Common;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Extentions;
using MyApp.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MyApp.Application.Common.Service
{
    public class ProductIdentityGenerator : IProductIdentityGenerator
    {
        public string GenerateSlug(string input)
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

        public string GenerateSku(string? inputSku = null, string prefix = "PRD")
        {
            // 1. Nếu user nhập SKU
            if (!string.IsNullOrWhiteSpace(inputSku))
            {
                var sku = Normalize(inputSku);

                if (!IsValidSku(sku))
                    throw new ArgumentException("SKU không hợp lệ");

                return sku;
            }

            return GenerateUnique.GenerateUniqueSku(prefix); 
        }

        private static string Normalize(string input)
            => input.Trim().ToUpper();

        private static bool IsValidSku(string sku)
            => System.Text.RegularExpressions.Regex
                .IsMatch(sku, @"^[A-Z0-9]+(-[A-Z0-9]+)*$");
    }


}
