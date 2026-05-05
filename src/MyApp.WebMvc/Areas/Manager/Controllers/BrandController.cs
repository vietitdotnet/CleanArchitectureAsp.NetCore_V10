using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Brands;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;
        public BrandController(ILogger<BaseController> logger, IBrandService brandService) : base(logger)
        {
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchBrands(string term, CancellationToken ct)
        {
            var param = new BrandParamesters { KeySearch = term };

            var result = await _brandService.GetBrandLookupsAsync(param, ct);
            return Json(result.Select(x => new {
                id = x.Id,
                name = x.Name,
            }));
        }
    }
}
