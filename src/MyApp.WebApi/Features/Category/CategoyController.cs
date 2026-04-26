using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Categorys;
using MyApp.Application.Features.Categorys.Reponses;
using MyApp.Application.Features.Products.Responses;

namespace MyApp.WebApi.Features.Category
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoyController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoyController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetCategorysRespose), StatusCodes.Status200OK)]
        public async Task<GetCategorysRespose> GetCategorys(CancellationToken ct)
        {
            var result = await _categoryService.GetCategorysAsync(ct);

            return new GetCategorysRespose(result);
        }
    }
}
