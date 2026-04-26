using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Features.Promotions.DTOs.View;
using MyApp.Application.Features.Promotions.MapRaws;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class PromotionProfile : BaseProfile
    {
        public PromotionProfile()
        {
          

            CreateMap<Promotion, PromotionViewDto>()
                    .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId.ToString()))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                    .ForMember(dest => dest.TextType, opt => opt.MapFrom(src => src.Type.ToString()));


            CreateMap<Promotion, PromotionDto>()
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type))
                    .ForMember(dest => dest.TextType, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<Promotion, FlashSalePromotionDto>()
                    .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId.ToString()))
                    .ForMember(dest => dest.TextType, opt => opt.MapFrom(src => src.Type.ToString()));
             CreateMap<Promotion, RegularPromotionDto>()
                    .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId.ToString()))
                    .ForMember(dest => dest.TextType, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<Promotion, PromotionLookupDto>();



        }
    }
}
