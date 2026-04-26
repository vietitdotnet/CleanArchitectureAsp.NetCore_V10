using MyApp.Application.Features.PromotionItems.Requests;

namespace MyApp.WebMvc.Areas.Manager.Models.Promotions
{
    public class AddPromotionItemsViewModel
    {
        public int PromotionId { get; set; }

        public List<CreatePromotionItemRequest> Items { get; set; } = new();
    }
}
