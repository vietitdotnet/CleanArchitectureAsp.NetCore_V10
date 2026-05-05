using AutoMapper;
using MyApp.Application.Features.Taxs.DTOs;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Mappings
{
    public class TaxProfile : BaseProfile
    {
      public TaxProfile() 
      {
            CreateMap<Tax, TaxLookupDto>();
      }
    }
}
