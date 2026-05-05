using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Brands.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Brands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Brands
{
    public class BrandService : BaseService, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<IReadOnlyList<BrandLookupDto>> GetBrandLookupsAsync(BrandParamesters param, CancellationToken ct = default)
        {
            var spec = new BrandLookupSpec(param);

            var brands = await _unitOfWork.Repository<Brand, int>()
                .GetPagedAsync<BrandLookupDto, BrandParamesters>(spec, param, ct);

            return brands.Items;
        }
    }
}
