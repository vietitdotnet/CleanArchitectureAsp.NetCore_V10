using MyApp.Application.Common.Models;
using MyApp.Application.Features.Products.DTOs;

namespace MyApp.Application.Features.Products.Responses
{
    public class CreateProductResponse
    {
        public ProductDto Data { get; set; } = null!;

    }
}
