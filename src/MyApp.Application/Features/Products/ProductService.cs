using AutoMapper;
using MyApp.Application.Common.Queryable.Extentions;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Products.DTOs;
using MyApp.Application.Features.Products.DTOs.View;
using MyApp.Application.Features.Products.MapRaws;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Interfaces.Common;
using MyApp.Application.Specifications.Products;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications;
using MyApp.Domain.Specifications.Products;
using MyApp.Domain.Specifications.ProductUnits;


namespace MyApp.Application.Features.Products
{
    public class ProductService : BaseService, IProductService
    {

        private readonly ISlugService _slugService;
        public ProductService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceProvider serviceProvider,
            ISlugService slugService) 
            : base(unitOfWork, mapper, serviceProvider)
        {
            _slugService = slugService;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var spec = new ProductSpec();
            return  await _unitOfWork.Repository<Product, int>().ListProjectedAsync<ProductDto>(spec, ct);
        }

        public async Task<OperationResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var spec = new ProductByIdSpec(id);

            var result = await _unitOfWork.Repository<Product, int>().FirstOrDefaultAsync(spec);

            if (result == null)
                
                throw new NotFoundException($"Không tìm thấy ID");

            return OperationResult<ProductDto>.Ok(
               _mapper.Map<ProductDto>(result));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await _unitOfWork.Repository<Product, int>()
                            .GetByIdProjectedAsync<ProductDto>(id, ct);

            if (product == null) { throw new NotFoundException($"Không tìm thấy ID"); }

            return product;

        }

        public async Task<OperationResult<ProductDto>> CreateProductAsync
            (CreateProductRequest request,
            CancellationToken ct = default)
        {
            await ValidateAsync(request);


            var slug = await GenerateUniqueSlugAsync(request.Name);

            var product = Product.Create(
                request.Name,
                slug,
                request.Price!,
                request.Description,
                request.CategoryId
            );

            await _unitOfWork.Repository<Product, int>().AddAsync(product, ct);
            
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<ProductDto>.Ok(
                _mapper.Map<ProductDto>(product));
        }

        public async Task<PagedResponse<ProductDto, ProductParameters>> GetProductsAsync
            (ProductParameters param, CancellationToken  ct = default)
        {

          var spec = new ProductParametersSpec(param);

            var products = await _unitOfWork.Repository<Product, int>()
                .GetPagedAsync<ProductDto, ProductParameters>(spec, param, ct);

            return products;

        }

        public async Task<PagedResponse<ProductDto, ProductParameters>> GetProductsBySlugCategoryAsync
            (string categorySlug, ProductParameters param , CancellationToken ct = default)
        {

          throw new NotImplementedException();

        }

        public async Task<OperationResult<ProductDto>> UpdateProductAsync(int id , UpdateProductRequest request, CancellationToken ct)
        {
            await ValidateAsync(request);

            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id, ct);

            if(product == null)
                throw new NotFoundException($"Không tìm thấy ID");

            product.Update(
                request.Name,
                request.Slug,
                request.Price,
                request.Description,
                request.CategoryId);

             _unitOfWork.Repository<Product, int>().Update(product);

            await _unitOfWork.SaveChangesAsync(ct);

            return OperationResult<ProductDto>.Ok(
                _mapper.Map<ProductDto>(product));

        }

        public async Task<OperationResult<bool>> DeleteProductAsync(int id,
         CancellationToken ct = default)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id, ct);

            if (product == null)
                throw new NotFoundException($"Không tìm thấy ID");

             _unitOfWork.Repository<Product, int>().Delete(product);

            await _unitOfWork.SaveChangesAsync(ct);

            return OperationResult<bool>.Ok(true);
        }

        public async Task<ProductViewDto?> GetProductBySlugAsync(string slug, CancellationToken ct = default)
        {
           
            var spec = new ProductBySlugSpec(slug);
           
            var raw = await _unitOfWork.Repository<Product, int>()
                .FirstOrDefaultProjectedAsync<ProductDetailRaw>(spec, DateTimeOffset.UtcNow , ct);
            if (raw == null)
                throw new NotFoundException($"Không tìm thấy dữ liệu với slug: {slug}");

            return ProductViewDto.Create(raw);
        }



        private async Task<string> GenerateUniqueSlugAsync(string name)
        {
            var baseSlug = _slugService.Generate(name);
            var slug = baseSlug;
            var index = 1;

            while (await _unitOfWork.Repository<Product, int>()
                .AnyAsync(new ProductBySlugSpec(slug)))
            {
                slug = $"{baseSlug}-{index++}";
            }

            return slug;
        }


    }
}
