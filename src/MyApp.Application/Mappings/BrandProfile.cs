using MyApp.Application.Features.Brands.DTOs;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class BrandProfile : BaseProfile
    {
        public BrandProfile()
        {
                CreateMap<Brand, BrandLookupDto>().ReverseMap();
        }
    }
}
