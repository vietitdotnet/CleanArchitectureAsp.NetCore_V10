using MyApp.Application.Features.ProductUints.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Products.Responses
{
    public class GetProductLookupResponse
    {
        public IReadOnlyList<ProductUnitLookupDto> Data { get; private set; }

            public GetProductLookupResponse(IReadOnlyList<ProductUnitLookupDto> data)
            {
                Data = data;
        }
    }
}
