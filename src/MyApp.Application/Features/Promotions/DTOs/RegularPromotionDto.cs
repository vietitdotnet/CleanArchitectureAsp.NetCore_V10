using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.DTOs
{
    public class RegularPromotionDto : BaseDto
    {
        public string PublicId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Value { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsOnlineOnly { get; set; }
        public bool IsActive { get; set; }
        public string TextType {  get; set; } = null!;
    }
}
