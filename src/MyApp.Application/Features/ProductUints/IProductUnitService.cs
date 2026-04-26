using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints
{
    public interface IProductUnitService
    {
        Task<IReadOnlyList<ProductUnitLookupDto>> GetProductUnitLookupsAsync(ProductUnitParameters param, CancellationToken ct = default);

        Task<ProductUnitDto?> GetProductUnitByIdAsync(int id, CancellationToken ct = default);
    }
}
