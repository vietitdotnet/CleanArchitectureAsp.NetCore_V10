using MyApp.Application.Features.Categorys.DTOs;
using MyApp.Application.Features.Taxs.DTOs;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Categorys
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetCategorysAsync(CancellationToken ct = default);

        Task<IReadOnlyList<CategoryLookupDto>> GetCategoryLookupsAsync(CategoryParameters param, CancellationToken ct = default);
    }
}