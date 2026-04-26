using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.ProductUnits;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints
{
    public class ProductUnitService : BaseService, IProductUnitService
    {
        public ProductUnitService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<ProductUnitDto?> GetProductUnitByIdAsync(int id, CancellationToken ct = default)
        {
           var spec = new ProductUnitByIdSpec(id);

            var productUnitDto = await _unitOfWork.Repository<ProductUnit, int>()
                .FirstOrDefaultProjectedAsync<ProductUnitDto>(spec, ct)
                ?? throw new NotFoundException("Product unit not found.");
            return productUnitDto;
        }

        public async Task<IReadOnlyList<ProductUnitLookupDto>> GetProductUnitLookupsAsync(ProductUnitParameters param, CancellationToken ct = default)
        {
            var spec = new ProductUnitFilterSpec(param);

            var productLookupDtos = await _unitOfWork.Repository<ProductUnit, int>()
                .GetPagedAsync<ProductUnitLookupDto, ProductUnitParameters>(spec, param, ct);

            return productLookupDtos.Items;

        }
    }
}
