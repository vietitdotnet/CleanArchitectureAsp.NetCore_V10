using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.ProductUints;
using MyApp.Application.Features.PromotionItems;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.Promotions;
using MyApp.Application.Features.Promotions.Requests;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Parameters;
using MyApp.WebMvc.Areas.Manager.Models.Promotions;
using MyApp.WebMvc.Extentions;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class PromotionController : BaseController
    {
        private readonly IPromotionService _promotionService;

        private readonly IProductUnitService _productUnitService;

        private readonly IPromotionItemService _promotionItemService;
        public PromotionController
            (ILogger<BaseController> logger, 
            IPromotionService promotionService, 
            IProductUnitService productUnitService, IPromotionItemService promotionItemService ) : base(logger)
        {
            _promotionService = promotionService;
            _productUnitService = productUnitService;
            _promotionItemService = promotionItemService;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PromotionParameters param, CancellationToken ct)
        {
          
            var result = await _promotionService.GetPromotionsAsync(param, ct);

            return View(result);
        }

        [HttpGet]

        public async Task<IActionResult> CreateFlashSale(CancellationToken ct)
        {
            
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateFlashSale(CreateFlashSaleRequest request, CancellationToken ct)
        {
            
            
            var result = await _promotionService.CreateFlashSalePromotionAsync(request, ct);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Tạo flash sale thành công!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddErrors(result);

            return View(request);

        }

        [HttpGet]
        public async Task<IActionResult> CreateRegularPromotion(CancellationToken ct)
        {
          
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateRegularPromotion(CreateRegularPromotionRequest request, CancellationToken ct)
        {

            var result = await _promotionService.CreateRegularPromotionAsync(request, ct);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Tạo promotion thành công!";
                return RedirectToAction(nameof(Index));
            }
 
            ModelState.AddErrors(result);

            return View(request);

        }




        [HttpGet]

        public async Task<IActionResult> AddPromotionItems([FromRoute] int id, [FromQuery] PromotionItemParameters param, CancellationToken ct)
        {

            var promotion = await _promotionService.GetPromotionByIdAsync(id, ct);

            if (promotion == null) return NotFound("Không tìm thấy khuyến mãi.");

            var promotionItems = await _promotionItemService.GetPromotionItemsByPromotionIdAsync(id, param, ct);

            return View(new PromotionItemsViewModel(promotion.Id, promotion.Name, promotionItems));

        }

        [HttpPost]
        public async Task<ActionResult> ToggleActive(int id, bool isActive, CancellationToken ct)
        {
           var result = await _promotionItemService.SetActiveAsync(id, isActive, ct);

           return Json(new { success = result.Success, message = result.Message });

        }

        [HttpGet]
        public async Task<ActionResult> CreatePromotionItem(
                                      [FromRoute] int id,
                                      [FromQuery] int unitId,
                                      string? returnUrl,
                                      CancellationToken ct)
        {
            ReturnUrl = returnUrl ?? Url.Action(nameof(AddPromotionItems), new { id });

            var productunit = await _productUnitService.GetProductUnitByIdAsync(unitId, ct);

            if (productunit == null) return NotFound();

            return View(new CreatePromotionItemRequest()
            {
                ProductUnitId = productunit.Id,
                OriginalPrice = productunit.SellingPrice
            });
        }

        [HttpPost]
        public async Task<ActionResult> CreatePromotionItem([FromRoute] int id, CreatePromotionItemRequest request, string? returnUrl, CancellationToken ct)
        {
            var result = await _promotionItemService.CreatePromotionItemAsync(id, request, ct);

            if (result.Success)
            {
                StatusMessage = "Thêm sản phẩm vào khuyến mãi thành công!";
          
                return RedirectToAction(nameof(AddPromotionItems), new { id = result.Data.PromotionId });
            }

            ModelState.AddErrors(result);
            
            ReturnUrl = returnUrl;

            return View(request);
        }

        [HttpGet]

        public async Task<ActionResult> UpdatePromotionItem([FromRoute] int id , [FromQuery] int promotionId , string? returnUrl, CancellationToken ct)
        {
            ReturnUrl = returnUrl ?? Url.Action(nameof(AddPromotionItems), new { id = promotionId });

            var promotionItem = await _promotionItemService.GetPromotionItemByIdAsync(id, ct);
                            
            var request = new UpdatePromotionItemRequest()
            {
                OriginalPrice = promotionItem.OriginalPrice,
                OverrideValue = promotionItem.OverrideValue,
                IsPercentageOverride = promotionItem.IsPercentageOverride
            };

            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePromotionItem([FromRoute] int id, UpdatePromotionItemRequest request, string? returnUrl, CancellationToken ct)
        {
            
            var result = await _promotionItemService.UpdatePromotionItemAsync(id, request, ct);

            if (result.Success)
            {
                StatusMessage = "Cập nhật mục khuyến mãi thành công!";
                return RedirectToAction(nameof(AddPromotionItems), new { id = result.Data.PromotionId });
            }

            ModelState.AddErrors(result);

            ReturnUrl = returnUrl;

            return View(request);
        }

        [HttpPost]
        public async Task<ActionResult> DeletePromotionItem(int id, CancellationToken ct)
        {
            var result = await _promotionItemService.DeletePromotionItemAsync(id, ct);

            return Json(new { success = result.Success, message = result.Message });
        }

        [HttpGet]
        public async Task<ActionResult> SearchProductUnits(string term, CancellationToken ct)
        {
            var param = new ProductUnitParameters{KeySearch = term};

            var result = await _productUnitService.GetProductUnitLookupsAsync(param, ct);

            return Json(result.Select(x => new {
                id = x.Id,
                text = x.FullName,
                productName = x.ProductName,
                unitName = x.UnitName
            }));
        }

    }
}