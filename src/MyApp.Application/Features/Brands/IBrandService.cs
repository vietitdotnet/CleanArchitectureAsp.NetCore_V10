using MyApp.Application.Features.Brands.DTOs;
using MyApp.Application.Features.Products.DTOs;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Brands
{
    public interface IBrandService
    {
        Task<IReadOnlyList<BrandLookupDto>> GetBrandLookupsAsync(BrandParamesters param, CancellationToken ct = default);

    }
}
