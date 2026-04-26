using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyApp.Domain.Paginations.Parameters
{
    public class ProductUnitParameters : PagingParameters
    {
        private const int MaxKeywordLength = 30;

        public string? KeySearch { get; set; }

        public override void Normalize()
        {
            if (!string.IsNullOrWhiteSpace(KeySearch))
            {
                KeySearch = KeySearch.Trim();

                if (KeySearch.Length > MaxKeywordLength)
                {
                    KeySearch = KeySearch.Substring(0, MaxKeywordLength);
                }
            }
        }
    }
}
