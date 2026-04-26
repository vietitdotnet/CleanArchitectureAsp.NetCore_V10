using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Paginations.Contants
{
    public readonly record struct PromotionSort(string Value)
    {

        public static PromotionSort None => new(string.Empty);

        public static PromotionSort StartDateAsc => new("start_asc");
        public static PromotionSort StartDateDesc => new("start_desc");
        public static bool TryParse(string? value, out PromotionSort sort)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                sort = None;
                return false;
            }
            sort = value.Trim().ToLowerInvariant() switch
            {
                "start_asc" => StartDateAsc,
                "start_desc" => StartDateDesc,
                _ => None
            };

            return !sort.IsNone;
        }

        public bool IsNone => string.IsNullOrEmpty(Value);
    }
}
