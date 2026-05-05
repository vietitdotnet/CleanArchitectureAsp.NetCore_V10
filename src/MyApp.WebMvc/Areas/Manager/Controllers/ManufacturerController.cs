using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Manufacturers;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerService _manufacturerService;
        public ManufacturerController(ILogger<BaseController> logger, IManufacturerService manufacturerService) : base(logger)
        {
            _manufacturerService = manufacturerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchManufacturers(string term, CancellationToken ct)
        {
            var param = new ManufacturerParameters { KeySearch = term };

            var result = await _manufacturerService.GetManufacturerLookupsAsync(param, ct);
            return Json(result.Select(x => new {
                id = x.Id,
                name = x.Name,
            }));
        }
    }
}
