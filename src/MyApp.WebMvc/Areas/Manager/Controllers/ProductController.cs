using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products;
using MyApp.Application.Features.Products.Requests;
using MyApp.Domain.Paginations.Parameters;
using MyApp.WebMvc.Extentions;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{

    public class ProductController : BaseController
    {


        private readonly IProductService _productService;

        public ProductController(ILogger<BaseController> logger, IProductService productService) : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] ProductParameters param, CancellationToken ct)
        {

            var result = await _productService.GetProductsAsync(param, ct);

            return View(result);
        }
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {

            var result = await _productService.CreateProductAsync(request);

            if (result.Success)
            {
                StatusMessage = "Tạo sản phẩm thành công";

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddErrors(result);

            return View();

        }



        [HttpGet]
        public async Task<ActionResult> SearchProducts(string term, CancellationToken ct)
        {
            var param = new ProductParameters { KeySearch = term };

            var result = await _productService.GetProductLookupsAsync(param, ct);

            return Json(result.Select(x => new {
                id = x.Id,
                text = x.Name,
                sku = x.Sku,
                barcode = x.Barcode,
            }));
        }

    }
}
