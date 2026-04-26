using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Models.Promotions
{
    public class PromotionItemsViewModel
    {
        public int PromotionId { get; private set; }

        public string PromotionName { get; private set; } = null!;

        public PagedResponse<PromotionItemDto, PromotionItemParameters> PromotionItems { get; private set; }=null!;


        public PromotionItemsViewModel(int promotionId, string promotionName , PagedResponse<PromotionItemDto, PromotionItemParameters> promotionItems )
        {
            PromotionId = promotionId;
            PromotionName = promotionName;
            PromotionItems = promotionItems;
        }
    }
}
