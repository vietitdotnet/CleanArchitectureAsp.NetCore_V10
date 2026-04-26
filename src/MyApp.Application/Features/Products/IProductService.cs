using MMyApp.Application.Features.Products.Responses;
using MyApp.Application.Common.Results;
using MyApp.Application.Features.Products.DTOs;
using MyApp.Application.Features.Products.DTOs.View;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;


namespace MyApp.Application.Features.Products
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(CancellationToken ct = default);
        


        Task<ProductDto> GetProductByIdAsync(int id, CancellationToken ct = default);

        Task<ProductViewDto?> GetProductBySlugAsync(string slug, CancellationToken ct = default);

        Task<PagedResponse<ProductDto, ProductParameters>> GetProductsAsync(ProductParameters param ,CancellationToken ct = default);
        
        Task<OperationResult<ProductDto>> CreateProductAsync(CreateProductRequest request, 
          CancellationToken ct = default );
        
        Task<OperationResult<bool>> DeleteProductAsync(int id, CancellationToken ct = default);

        Task<OperationResult<ProductDto>> UpdateProductAsync(int id , UpdateProductRequest request,
            CancellationToken ct = default);

        Task<PagedResponse<ProductDto, ProductParameters>> GetProductsBySlugCategoryAsync(
            string categorySlug, 
            ProductParameters param, CancellationToken ct = default);

       
    }
}
