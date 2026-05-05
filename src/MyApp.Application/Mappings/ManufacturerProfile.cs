using MyApp.Application.Features.Manufacturers.DTOs;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class ManufacturerProfile : BaseProfile
    {
        public ManufacturerProfile() 
        {
            CreateMap<Manufacturer, ManufacturerLookupDto>();
                

        }
    }
}
