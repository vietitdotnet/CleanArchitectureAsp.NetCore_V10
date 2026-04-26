using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities.Districts
{
    public class AdministrativeLevel : BaseEntity<int>
    {

        // ví dụ: CITY, PROVINCE, WARD, COMMUNE
        public string Code { get; set; } = null!;

        // ví dụ: "Thành phố Trung ương", "Tỉnh", "Phường"
        public string Name { get; set; } = null!;

        public ICollection<Province> Provinces { get; set; } = new List<Province>();
        public ICollection<Commune> Communes { get; set; } = new List<Commune>();
    }
}
