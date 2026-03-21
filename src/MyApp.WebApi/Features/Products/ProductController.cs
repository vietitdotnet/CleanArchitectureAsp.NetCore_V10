using Microsoft.AspNetCore.Mvc;
using MMyApp.Application.Features.Products.Responses;
using MyApp.Application.Features.Products;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.Products.Responses;
using MyApp.Domain.Paginations.Parameters;


namespace MyApp.WebApi.Features.Products
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
        public async Task<GetAllProductsResponse> GetAllProducts(CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(ct);

             return new GetAllProductsResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductsResponse) ,StatusCodes.Status200OK)]
        public async Task<GetProductsResponse> GetProducts([FromQuery] ProductParameters praram, CancellationToken ct)
        {
           var result =  await _productService.GetProductsAsync(praram ,ct);

            return new GetProductsResponse(result);
        }
           

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetProductResponse) ,StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProductResponse>> GetProductById([FromRoute]int id,CancellationToken ct)
        {
            var product = await _productService.GetProductByIdAsync(id, ct);

            return Ok(new GetProductResponse(product));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse) ,StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct(
            [FromBody] CreateProductRequest req)
        {
            var result = await _productService.CreateProductAsync(req);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = result.Data.Id },
                result.Data
            );
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(UpdateProductResponse) ,StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(int id, [FromBody] UpdateProductRequest req)
        {
            var result = await _productService.UpdateProductAsync(id, req);

            return Ok(result);
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
        public async Task<GetProductsResponse> GetProductsBySlugCategory([FromRoute] string slug, [FromQuery] ProductParameters praram, CancellationToken ct)
        {
            praram.Normalize();

            var result = await _productService.GetProductsBySlugCategoryAsync(slug, praram, ct);

            return new GetProductsResponse(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }

}
