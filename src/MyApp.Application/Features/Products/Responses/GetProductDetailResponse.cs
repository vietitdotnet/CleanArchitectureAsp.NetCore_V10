using MyApp.Application.Features.Products.DTOs.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Products.Responses
{
    public class GetProductDetailResponse
    {
        public ProductViewDto Data { get; set; }

        public GetProductDetailResponse(ProductViewDto data)
        {
            Data = data;
        }

    }
}
