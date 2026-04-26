using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities.Districts
{
    public class Commune : BaseEntity<int>
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? EnglishName { get; set; }

        public int AdministrativeLevelId { get; set; }
        public AdministrativeLevel AdministrativeLevel { get; set; } = null!;
        public string ProvinceCode { get; set; } = null!;
        public Province Province { get; set; } = null!;

    }
}
