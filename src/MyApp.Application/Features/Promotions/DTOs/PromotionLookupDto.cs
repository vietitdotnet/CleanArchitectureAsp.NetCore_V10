using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.DTOs
{
    public class PromotionLookupDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    
    }
}
