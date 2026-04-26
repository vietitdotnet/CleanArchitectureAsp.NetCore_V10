using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Medicine : BaseEntity<int>
    {
        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public string? Dosage { get; private set; } // liều dùng
        public string? Contraindications { get; private set; } // chống chỉ định

        public MedicineType MedicineType { get; private set; } = MedicineType.OTC;

        private Medicine() { }

        public Medicine(int productId, MedicineType type, string? dosage = null, string? contraindications = null)
        {
            ProductId = productId;
            MedicineType = type;
            Dosage = dosage;
            Contraindications = contraindications;
        }

    }
}
