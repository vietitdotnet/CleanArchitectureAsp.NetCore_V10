using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Common.Reponses;
using MyApp.Application.Features.PromotionItems;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.PromotionItems.Respones;

namespace MyApp.WebApi.Features.PromotionItems
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PromotionItemController : BaseApiController
    {
        private readonly IPromotionItemService _promotionItemService;   

        public PromotionItemController(IPromotionItemService promotionItemService)
        {
            _promotionItemService = promotionItemService;
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeletePromotionItem(int id, CancellationToken ct)
        {
           var result = await _promotionItemService.DeletePromotionItemAsync(id, ct);
            
           return Ok(new ApiResponse(result.Success, result.Message));

        }

        // TOGGLE ACTIVE
        [HttpPatch("{id:int}/active")]
        public async Task<ActionResult<ApiResponse>> SetActive(int id, [FromBody] SetActiveRequest req, CancellationToken ct)
        {
            var result = await _promotionItemService.SetActiveAsync(id, req.IsActive, ct);
            
            return Ok(new ApiResponse(result.Success, result.Message));
        }

        // UPDATE
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdatePromotionItemRespone>> UpdatePromotionItem(int id, [FromBody] UpdatePromotionItemRequest req, CancellationToken ct)
        {
            var result = await _promotionItemService.UpdatePromotionItemAsync(id, req, ct);
            
            if (!result.Success)
            {
                return BadRequestResult(result);
            }

            return Ok(new UpdatePromotionItemRespone(result.Data));

        }
    }
}
