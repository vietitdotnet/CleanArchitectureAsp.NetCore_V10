
using MyApp.Application.Features.Products.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;

namespace MMyApp.Application.Features.Products.Responses
{
    public class GetProductsResponse
    {
        public PagedResponse<ProductDto, ProductParameters> Data { get; private set; } = null!;

        public GetProductsResponse(PagedResponse<ProductDto, ProductParameters>  pagedProducts)
        {
            Data = pagedProducts;
        }

    }
}
