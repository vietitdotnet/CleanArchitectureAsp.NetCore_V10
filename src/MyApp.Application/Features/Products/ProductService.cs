using AutoMapper;
using MyApp.Application.Common.Queryable.Extentions;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Products.DTOs;
using MyApp.Application.Features.Products.MapRaws;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.Products.Views;
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
            var validationResult = await ValidateAsync(request);

            if (!validationResult.Success)
            {
                return OperationResult<ProductDto>.Fails(validationResult.Errors!, "Dữ liệu không hợp lệ");
            }

            if (!string.IsNullOrWhiteSpace(request.Sku))
            {
                var skuExists = await _unitOfWork
                    .Repository<Product, int>()
                    .AnyAsync(new ProductBySkuSpec(request.Sku));

                if (skuExists)
                    return OperationResult<ProductDto>.Fail($"SKU '{request.Sku}' đã tồn tại");
            }

            var slug = await GenerateUniqueSlugAsync(request.Name);

            var product = Product.Create( 
                slug, 
                request.Name, 
                request.ShortName,
                request.CostPrice, 
                request.BasePrice,
                request.PackingSize,
                request.Benefit,
                request.BrandId,
                request.Sku, 
                request.Barcode, 
                request.ShortDescription, 
                request.Description,              
                request.RegistrationNumber, 
                request.DosageForm, 
                request.Ingredient, 
                request.CategoryId, 
                request.ManufacturerId,
                request.TaxId);

            product.AddUnit(request.UnitName, 1, request.SellingPrice, true, request.Barcode);
            

            await _unitOfWork.Repository<Product, int>().AddAsync(product, ct);

            await _unitOfWork.SaveChangesAsync(ct);


            return OperationResult<ProductDto>.Ok(
                _mapper.Map<ProductDto>(product),
                "Tạo sản phẩm thành công");

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
            throw new NotImplementedException();


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

        public async Task<ProductViewDto> GetProductBySlugAsync(string slug, CancellationToken ct = default)
        {
           
            var spec = new ProductBySlugSpec(slug);
           
            var raw = await _unitOfWork.Repository<Product, int>()
                .FirstOrDefaultProjectedAsync<ProductDetailRaw>(spec, DateTimeOffset.UtcNow , ct);
            if (raw == null)
                throw new NotFoundException($"Không tìm thấy dữ liệu với slug: {slug}");

            return ProductViewDto.Create(raw);
        }



        public async Task<IReadOnlyList<ProductLookupDto>> GetProductLookupsAsync(ProductParameters param, CancellationToken ct = default)
        {
            var spec = new ProductLookupSpec(param);

            var products = await _unitOfWork.Repository<Product, int>()
                 .GetPagedAsync<ProductLookupDto, ProductParameters>(spec, param, ct);

            return products.Items;
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

        public async Task<ProductDetailDto> GetProductDetailByIdAsync(int id, CancellationToken ct = default)
        {
           var spec = new ProductByIdSpec(id);
           var product = await _unitOfWork.Repository<Product, int>()
                .FirstOrDefaultProjectedAsync<ProductDetailDto>(spec, ct);

            if (product == null)
                throw new NotFoundException($"Không tìm thấy ID");

            return product;
        }
    }
}
