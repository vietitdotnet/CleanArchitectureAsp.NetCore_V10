using MyApp.Application.Features.Promotions.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.Responses
{
    public class CreatePromotionResponse
    {
        public PromotionDto Data { get; private set; }
        public CreatePromotionResponse(PromotionDto data)
        {
            Data = data;
        }
    }
}
