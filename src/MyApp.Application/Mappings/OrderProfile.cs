using AutoMapper;
using MyApp.Application.Features.Authentications.DTOs;
using MyApp.Application.Features.OrderProducts.DTOs;
using MyApp.Application.Features.Orders.DTOs;
using MyApp.Domain.Abstractions.Users;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.TotalCount,
                    o => o.MapFrom(s => s.OrderProducts.Sum(p => p.Quantity)))
                .ForMember(d => d.TotalPrice,
                    o => o.MapFrom(s => s.OrderProducts.Sum(p => p.Price * p.Quantity)))
                .ForMember(dest => dest.Products,
                         o => o.MapFrom(s => s.OrderProducts))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.User));
               

            CreateMap<OrderProduct, OrderProductDto>()
                    .ForMember(dest => dest.ProductName,
                        opt => opt.MapFrom(src => src.Product.Name));
             
        }
    }

}
