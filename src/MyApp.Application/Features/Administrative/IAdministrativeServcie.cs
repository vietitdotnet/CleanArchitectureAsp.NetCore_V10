using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Administrative
{
    public interface  IAdministrativeServcie
    {
        Task SeedAsync(CancellationToken ct = default);
    }
}
