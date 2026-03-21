
using MyApp.Application.Features.Products.DTOs;

namespace MyApp.Application.Features.Products.Responses
{
    public class GetProductResponse
    {
        public ProductDto Data { get; private  set; } = null!;

        public GetProductResponse(ProductDto data) 
        {
            Data = data;
        }
    }
}
