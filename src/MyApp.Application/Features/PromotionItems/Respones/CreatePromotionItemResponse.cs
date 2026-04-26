using MyApp.Application.Common.Results;
using MyApp.Application.Features.PromotionItems.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.Respones
{
    public class CreatePromotionItemResponse
    {
        public OperationResult<PromotionItemDto> Data { get; private set; }


        public CreatePromotionItemResponse(OperationResult<PromotionItemDto> data)
        {
            Data = data;
        }
    }
}
