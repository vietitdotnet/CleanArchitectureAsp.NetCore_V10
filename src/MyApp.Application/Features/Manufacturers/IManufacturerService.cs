using MyApp.Application.Features.Manufacturers.DTOs;

using MyApp.Domain.Paginations.Parameters;


namespace MyApp.Application.Features.Manufacturers
{
    public interface IManufacturerService
    {
        Task<IReadOnlyList<ManufacturerLookupDto>> GetManufacturerLookupsAsync(ManufacturerParameters param, CancellationToken ct = default);
    }
}
