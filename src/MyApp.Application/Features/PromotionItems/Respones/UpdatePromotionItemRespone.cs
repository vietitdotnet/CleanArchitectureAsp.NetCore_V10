using MyApp.Application.Features.PromotionItems.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.Respones
{
    public class UpdatePromotionItemRespone
    {
        public PromotionItemDto Data { get; private set; }

        public UpdatePromotionItemRespone(PromotionItemDto data)
        {
            Data = data;
        }
    }
}
