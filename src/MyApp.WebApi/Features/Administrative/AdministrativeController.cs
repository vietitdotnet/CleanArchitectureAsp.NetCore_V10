using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Administrative;

namespace MyApp.WebApi.Features.Administrative
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministrativeController : ControllerBase
    {
        private readonly IAdministrativeServcie _service;

        public AdministrativeController(IAdministrativeServcie service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Seed(CancellationToken ct)
        {
            await _service.SeedAsync(ct);
            return Ok(new { message = "Seed thành công" });
        }
    }
}
