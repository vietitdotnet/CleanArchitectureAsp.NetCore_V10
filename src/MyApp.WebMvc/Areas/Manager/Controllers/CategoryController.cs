using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Categorys;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<BaseController> logger, ICategoryService categoryService) : base(logger)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchCategorys(string term, CancellationToken ct)
        {
            var param = new CategoryParameters { KeySearch = term };

            var result = await _categoryService.GetCategoryLookupsAsync(param, ct);
           
            return Json(result.Select(x => new {
                id = x.Id,
                name = x.Name,
            }));
        }
    }
}
