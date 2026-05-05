using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Manufacturers.DTOs;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Manufacturers;
using MyApp.Domain.Specifications.ProductUnits;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Manufacturers
{
    public class ManufacturerService : BaseService, IManufacturerService
    {
        public ManufacturerService(IUnitOfWork unitOfWork, 
            IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<IReadOnlyList<ManufacturerLookupDto>> GetManufacturerLookupsAsync(ManufacturerParameters param, CancellationToken ct = default)
        {
            var spec = new ManufacturerLookupSpec(param);

            var manufacturers = await _unitOfWork.Repository<Manufacturer, int>()
                .GetPagedAsync<ManufacturerLookupDto, ManufacturerParameters>(spec, param, ct);

            return manufacturers.Items;

        }
    }
}
