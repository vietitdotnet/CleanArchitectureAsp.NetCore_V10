using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class PromotionItemProfile : BaseProfile
    {
        public PromotionItemProfile()
        {
            CreateMap<PromotionItem, PromotionItemRaw>()
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Promotion.Name))
                   .ForMember(dest => dest.IsPercentage, opt => opt.MapFrom(src => src.Promotion.IsPercentage))
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Promotion.Value))
                   .ForMember(dest => dest.MaxDiscountAmount, opt => opt.MapFrom(src => src.Promotion.MaxDiscountAmount))
                   .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Promotion.StartDate))
                   .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Promotion.EndDate))
                   .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.Promotion.PublicId));

            CreateMap<PromotionItem, PromotionItemDto>();
                
        }
    }
}
