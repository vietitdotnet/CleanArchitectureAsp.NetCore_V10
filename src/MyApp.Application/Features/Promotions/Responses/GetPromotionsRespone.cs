using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.Responses
{
    public class GetPromotionsResponse
    {
        public PagedResponse<PromotionDto, PromotionParameters> Data { get; private set; }


        public GetPromotionsResponse(PagedResponse<PromotionDto, PromotionParameters> data)
        {
            Data = data;
        }
    }
}
