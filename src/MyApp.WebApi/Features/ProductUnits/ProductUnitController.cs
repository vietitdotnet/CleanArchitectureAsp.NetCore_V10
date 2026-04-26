using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products.Responses;
using MyApp.Application.Features.ProductUints;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebApi.Features.ProductUnits
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductUnitController : ControllerBase
    {
        private readonly IProductUnitService _productUnitService;

        public ProductUnitController(IProductUnitService productUnitService)
        {
            _productUnitService = productUnitService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductLookupResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetProductLookupResponse>> GetProductLookups([FromQuery] ProductUnitParameters param, CancellationToken ct)
        {
            var result = await _productUnitService.GetProductUnitLookupsAsync(param, ct);
            return Ok(new GetProductLookupResponse(result));
        }



    }
}
