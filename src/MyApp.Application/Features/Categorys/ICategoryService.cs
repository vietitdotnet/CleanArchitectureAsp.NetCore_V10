using MyApp.Application.Features.Categorys.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Categorys
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetCategorysAsync(CancellationToken ct = default);
    }
}