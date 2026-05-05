using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Taxs.DTOs;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Taxs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Taxs
{
    public class TaxService : BaseService, ITaxService
    {
        public TaxService(IUnitOfWork unitOfWork, IMapper mapper, IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<IReadOnlyList<TaxLookupDto>> GetTaxLookupsAsync(TaxParameters param, CancellationToken ct = default)
        {
            var spec = new TaxLookupSpec(param);

            var taxs = await _unitOfWork.Repository<Tax, int>().GetPagedAsync<TaxLookupDto, TaxParameters>(spec, param, ct);

            return taxs.Items;
        }
    }
}
