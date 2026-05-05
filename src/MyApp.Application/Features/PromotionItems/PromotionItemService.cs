using AutoMapper;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.ProductUnits;
using MyApp.Domain.Specifications.PromotionItems;
using MyApp.Domain.Specifications.Promotions;

namespace MyApp.Application.Features.PromotionItems
{

    public class PromotionItemService : BaseService, IPromotionItemService
    {
        public PromotionItemService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task ActivateAsync(int id, CancellationToken ct)
        {
            var promotionItem = await _unitOfWork.Repository<PromotionItem, int>()
                .FirstOrDefaultAsync(new PromotionItemByIdSpec(id), ct);

            if (promotionItem == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            promotionItem.Activate();
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeactivateAsync(int id, CancellationToken ct)
        {
            var promotionItem = await _unitOfWork.Repository<PromotionItem, int>()
                .FirstOrDefaultAsync(new PromotionItemByIdSpec(id), ct);

            if (promotionItem == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            promotionItem.Deactivate();
            await _unitOfWork.SaveChangesAsync(ct);
        }


        public async Task<OperationResult<PromotionItemDto>> CreatePromotionItemAsync(
                     int promotionId,
                     CreatePromotionItemRequest request,
                     CancellationToken ct)
        {
            // 1. Kiểm tra Promotion (Tránh ID 0)
            var promoId = await _unitOfWork.Repository<Promotion, int>()
                .FirstOrDefaultWidthSelectorAsync(new PromotionByIdSpec(promotionId), x => x.Id, ct);

            if (promoId == 0) throw new NotFoundException("Không tìm thấy khuyến mãi.");

            // 2. Lấy thông tin ProductUnit qua Projection (Rất tốt!)
            var productUnit = await _unitOfWork.Repository<ProductUnit, int>()
                .FirstOrDefaultProjectedAsync<ProductUnitDto>(new ProductUnitByIdSpec(request.ProductUnitId), ct);

            if (productUnit == null)
                throw new NotFoundException($"Không tìm thấy đơn vị sản phẩm {request.ProductUnitId}.");

            var validateResult = await ValidateAsync(request, ct);
            if (!validateResult.Success)
                return OperationResult<PromotionItemDto>.Fails(validateResult.Errors!);

            // 3. (Optional) Check xem Item này đã tồn tại trong Promo chưa để tránh duplicate
            var validateDuplicate = await ValidateDuplicateAsync(promotionId, productUnit.Id, ct);
            if (!validateDuplicate.Success)
                return OperationResult<PromotionItemDto>.Fail(validateDuplicate.Message!);

            var checkOriginalPrice = ValidateFinalPrice(request.OriginalPrice, request.OverrideValue, request.IsPercentageOverride);

            if (!checkOriginalPrice.Success)
                return OperationResult<PromotionItemDto>.Fail(checkOriginalPrice.Message!);

           
            // 4. Tạo Item - Lấy OriginalPrice từ hệ thống thay vì Request để an toàn hơn
            var promotionItem = PromotionItem.Create(
                promoId,
                productUnit.Id,
                productUnit.ProductName,
                productUnit.UnitName,
                productUnit.SellingPrice,
                productUnit.Barcode,
                request.OverrideValue,
                request.IsPercentageOverride);


            await _unitOfWork.Repository<PromotionItem, int>().AddAsync(promotionItem, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return OperationResult<PromotionItemDto>.Ok(_mapper.Map<PromotionItemDto>(promotionItem));
        }



        public async Task<PromotionItemDto> GetPromotionItemByIdAsync(int id, CancellationToken ct)
        {
            var spec = new PromotionItemByIdSpec(id);

            var item = await _unitOfWork.Repository<PromotionItem, int>()
                .FirstOrDefaultProjectedAsync<PromotionItemDto>(spec, ct);

            if (item == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            return item;
        }

        public async Task<PagedResponse<PromotionItemDto, PromotionItemParameters>> GetPromotionItemsAsync(PromotionItemParameters param, CancellationToken ct = default)
        {

            var spec = new PromotionItemSpec();

            var items = await _unitOfWork.Repository<PromotionItem, int>()
                .GetPagedAsync<PromotionItemDto, PromotionItemParameters>(spec, param, ct);


            return items;
        }

        public async Task<PagedResponse<PromotionItemDto, PromotionItemParameters>> GetPromotionItemsByPromotionIdAsync(int promotionId, PromotionItemParameters param, CancellationToken ct)
        {
            var spec = new PromotionItemByPromotionIdSpec(promotionId);

            var items = await _unitOfWork.Repository<PromotionItem, int>()
                .GetPagedAsync<PromotionItemDto, PromotionItemParameters>(spec, param, ct);

            return items;
        }

        public async Task<OperationResult<PromotionItemDto>> UpdatePromotionItemAsync(int id, UpdatePromotionItemRequest request, CancellationToken ct)
        {
            var validateResult = await ValidateAsync(request, ct);

            if (!validateResult.Success)
                return OperationResult<PromotionItemDto>.Fails(validateResult.Errors!);

            var checkOriginalPrice = ValidateFinalPrice(request.OriginalPrice, request.OverrideValue, request.IsPercentageOverride);

            if (!checkOriginalPrice.Success)
                    return OperationResult<PromotionItemDto>.Fail(checkOriginalPrice.Message!);
            

            var promotionItem = await _unitOfWork.Repository<PromotionItem, int>()
                .FirstOrDefaultAsync(new PromotionItemByIdSpec(id), ct);

            if (promotionItem == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            promotionItem.UpdateOriginalPrice(request.OriginalPrice);

            promotionItem.UpdateOverride(request.OverrideValue, request.IsPercentageOverride);

            await _unitOfWork.SaveChangesAsync(ct);

            return OperationResult<PromotionItemDto>.Ok(_mapper.Map<PromotionItemDto>(promotionItem));
        }

        public async Task<OperationResult<bool>> SetActiveAsync(int id, bool isActive, CancellationToken ct)
        {
            var promotionItem = await _unitOfWork.Repository<PromotionItem, int>()
                 .FirstOrDefaultAsync(new PromotionItemByIdSpec(id), ct);

            if (promotionItem == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            if (isActive)
                promotionItem.Activate();
            else
                promotionItem.Deactivate();

            await _unitOfWork.SaveChangesAsync(ct);

            return OperationResult<bool>.Ok(isActive, $"Mục khuyến mãi đã {(isActive ? "kích hoạt" : "hủy kích hoạt")} thành công.");
        }

        public async Task<OperationResult<bool>> DeletePromotionItemAsync(int id, CancellationToken ct = default)
        {
            var promotionItem = await _unitOfWork.Repository<PromotionItem, int>()
                 .FirstOrDefaultAsync(new PromotionItemByIdSpec(id), ct);
            if (promotionItem == null) throw new NotFoundException($"Không tìm thấy mục khuyến mãi với ID {id}.");

            _unitOfWork.Repository<PromotionItem, int>().Delete(promotionItem);

            await _unitOfWork.SaveChangesAsync(ct);
            return OperationResult<bool>.Ok(true, $"Xóa mục khuyến mãi với ID {id} thành công.");
        }

        public async Task<OperationResult<bool>> ValidateDuplicateAsync(
       int promotionId,
       int productUnitId,
       CancellationToken ct)
        {
            var isExisted = await _unitOfWork
                .Repository<PromotionItem, int>()
                .AnyAsync(new PromotionItemByPromoAndUnitSpec(promotionId, productUnitId));

            if (isExisted)
            {
                return OperationResult<bool>.Fail(
                    $"Sản phẩm {productUnitId} đã tồn tại trong khuyến mãi này.");
            }

            return OperationResult<bool>.Ok(true);
        }

        public Task<PromotionItemDto> GetPromotionItemByPromoAndUnitAsync(int idPromotion, int productUnitId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        private OperationResult<bool> ValidateFinalPrice(
                decimal originalPrice,
                decimal? overrideValue,
                bool? isPercentageOverride)
        {
            // Không có override → luôn hợp lệ
            if (overrideValue == null && isPercentageOverride == null)
                return OperationResult<bool>.Ok(true);

            // Validate input cơ bản
            if (originalPrice < 0)
                return OperationResult<bool>.Fail("OriginalPrice không hợp lệ");

            if (overrideValue.HasValue && overrideValue.Value < 0)
                return OperationResult<bool>.Fail("OverrideValue không hợp lệ");

            // Nếu có overrideValue mà không rõ kiểu → coi như invalid
            if (overrideValue.HasValue && isPercentageOverride == null)
                return OperationResult<bool>.Fail("Thiếu thông tin loại giảm giá");

            // Nếu không có overrideValue → không cần tính
            if (!overrideValue.HasValue || overrideValue.Value <= 0)
                return OperationResult<bool>.Ok(true);

            // Tính giá cuối
            var finalPrice = CalculateFinalPrice(
                originalPrice,
                overrideValue,
                isPercentageOverride!.Value);

            // Rule chính
            if (finalPrice <= 0)
                return OperationResult<bool>.Fail("Giá sau giảm (dự kiến ) phải lớn hơn 0.");

            return OperationResult<bool>.Ok(true);
        }

        private decimal CalculateFinalPrice(decimal originalPrice, decimal? overrideValue, bool isPercentageOverride)
        {
            if (!overrideValue.HasValue || overrideValue <= 0)
                return originalPrice;

            return isPercentageOverride
                ? originalPrice * (1 - overrideValue.Value / 100)
                : originalPrice - overrideValue.Value;
        }

    }
}

