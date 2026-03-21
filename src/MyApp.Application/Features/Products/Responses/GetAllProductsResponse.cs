
using MyApp.Application.Features.Products.DTOs;

namespace MyApp.Application.Features.Products.Responses
{
    public class GetAllProductsResponse
    {
        public IReadOnlyList<ProductDto> Data { get; private set; } = null!;

        public GetAllProductsResponse(IReadOnlyList<ProductDto> products) 
        {
            Data = products;
        }
    }
}
