using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Tax : BaseEntity<int>
    {
        public string Name { get; private set; } = null!; // VAT 10%, VAT 5%
        public decimal Percentage { get; private set; } // 10, 5
        public ICollection<Product> Products { get; private set; } = [];
    }
}
