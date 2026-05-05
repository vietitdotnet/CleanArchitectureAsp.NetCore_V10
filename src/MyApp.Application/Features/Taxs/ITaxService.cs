using MyApp.Application.Features.Manufacturers.DTOs;
using MyApp.Application.Features.Taxs.DTOs;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Taxs
{
    public interface ITaxService
    {
        Task<IReadOnlyList<TaxLookupDto>> GetTaxLookupsAsync(TaxParameters  param, CancellationToken ct = default);
    }
}
