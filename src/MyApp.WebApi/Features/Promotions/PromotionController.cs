using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Common.Results;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.Products.Responses;
using MyApp.Application.Features.PromotionItems;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.PromotionItems.Respones;
using MyApp.Application.Features.Promotions;
using MyApp.Application.Features.Promotions.Responses;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Parameters;
using Org.BouncyCastle.Ocsp;

namespace MyApp.WebApi.Features.Promotions
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PromotionController : BaseApiController
    {
        private readonly IPromotionService _promotionService;

        private readonly IPromotionItemService _promotionItemService;

        public PromotionController(IPromotionService promotionService, IPromotionItemService promotionItemService)
        {
            _promotionService = promotionService;
            _promotionItemService = promotionItemService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetPromotionsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPromotionsResponse>> GetPromotions([FromQuery] PromotionParameters parameters, CancellationToken ct)
        {
            
            var result = await _promotionService.GetPromotionsAsync(parameters, ct);

            return Ok(new GetPromotionsResponse(result));
        }




        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetPromotionItemResponse), StatusCodes.Status200OK)]
      
        public async Task<ActionResult<GetPromotionItemResponse>> GetPromotionItemById([FromRoute] int id, CancellationToken ct)
        {
            var result = await _promotionItemService.GetPromotionItemByIdAsync(id, ct);
            return Ok(new GetPromotionItemResponse(result));
        }

        
        [HttpPost("{id:int}")]
        [ProducesResponseType(typeof(CreatePromotionItemResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult<CreatePromotionItemResponse>> CreatePromotionItem(
            [FromRoute] int id,
            [FromBody] CreatePromotionItemRequest req,
            CancellationToken ct)
        {
            var result = await _promotionItemService.CreatePromotionItemAsync(id, req, ct);

            if (!result.Success) return BadRequestResult(result);


            return CreatedAtAction(
                nameof(GetPromotionItemById),
                new { id = result.Data.Id }, // 'id' ở đây phải khớp chính xác với tham số của hàm Get
                new CreatePromotionItemResponse(result)
            );
        }


    }
}
