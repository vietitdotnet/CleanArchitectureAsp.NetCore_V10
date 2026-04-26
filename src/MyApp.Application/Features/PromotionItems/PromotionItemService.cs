using AutoMapper;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.ProductUints.DTOs;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.Promotions.DTOs;
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

            // 3. (Optional) Check xem Item này đã tồn tại trong Promo chưa để tránh duplicate
            var isExisted = await _unitOfWork.Repository<PromotionItem, int>().AnyAsync(new PromotionItemByPromoAndUnitSpec(promotionId, productUnit.Id));

            if (isExisted)
            {
                return OperationResult<PromotionItemDto>.Fail($"Sản phẩm {productUnit.Id} đã tồn tại trong khuyến mãi này.");
            }


            var validationResult = await ValidateAsync(request, ct);

            if (!validationResult.Success)
                return OperationResult<PromotionItemDto>.Fail(validationResult.Errors!);

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
            var validationResult = await ValidateAsync(request, ct);

            if (!validationResult.Success)
                return OperationResult<PromotionItemDto>.Fail(validationResult.Errors!);

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
            if (promotionItem == null) throw new NotFoundException("Không tìm thấy mục khuyến mãi.");

            _unitOfWork.Repository<PromotionItem, int>().Delete(promotionItem);

            await _unitOfWork.SaveChangesAsync(ct);
            return OperationResult<bool>.Ok(true, "Xóa mục khuyến mãi thành công.");
        }
    }
}
