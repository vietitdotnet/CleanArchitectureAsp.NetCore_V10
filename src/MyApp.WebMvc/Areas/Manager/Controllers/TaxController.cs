using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Taxs;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class TaxController : BaseController
    {

        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService, ILogger<BaseController> logger) : base(logger)
        {
            _taxService = taxService;
        }
 
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchTaxs(string term, CancellationToken ct)
        {
            var param = new TaxParameters { KeySearch = term };

            var result = await _taxService.GetTaxLookupsAsync(param, ct);

            return Json(result.Select(x => new {
                id = x.Id,
                name = x.Name,
            }));
        }

    }
}
