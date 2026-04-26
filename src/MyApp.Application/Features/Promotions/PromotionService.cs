using AutoMapper;
using MediatR;
using MyApp.Application.Common.Results;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Features.Promotions.Requests;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using MyApp.Domain.Specifications.Products;
using MyApp.Domain.Specifications.ProductUnits;
using MyApp.Domain.Specifications.Promotions;


namespace MyApp.Application.Features.Promotions
{
    public class PromotionService : BaseService, IPromotionService
    {
        public PromotionService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<OperationResult<FlashSalePromotionDto>> CreateFlashSalePromotionAsync(CreateFlashSaleRequest req, CancellationToken ct = default)
        {
            var validationResult = await ValidateAsync(req, ct);
           
            if (!validationResult.Success)
            {
                return OperationResult<FlashSalePromotionDto>.Fail(validationResult.Errors!);
            }

            var flashSale = Promotion.CreateFlashSale(
                                     req.Name, req.Value, req.IsPercentage, req.MaxDiscountAmount,
                                     req.StartDate, req.EndDate, req.LimitQuantity,
                                     req.DailyStartTime, req.DailyEndTime
                                     );

            await _unitOfWork.Repository<Promotion, int>().AddAsync(flashSale, ct);

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<FlashSalePromotionDto>.Ok(_mapper.Map<FlashSalePromotionDto>(flashSale), "Tạo khuyến mãi flash sale thành công.");
        }

        public async Task<OperationResult<RegularPromotionDto>> CreateRegularPromotionAsync(CreateRegularPromotionRequest request, CancellationToken ct = default)
        {
            var validationResult = await ValidateAsync(request, ct);

            if (!validationResult.Success)
            {
                return OperationResult<RegularPromotionDto>.Fail(validationResult.Errors!);
            }

            var regular = Promotion.CreatePromotion(request.Name,
                request.Value,
                request.IsPercentage, 
                request.MaxDiscountAmount,
                request.StartDate, request.EndDate,
                request.IsOnlineOnly);

            await _unitOfWork.Repository<Promotion, int>().AddAsync(regular, ct);

            await _unitOfWork.SaveChangesAsync();

            return OperationResult<RegularPromotionDto>.Ok(_mapper.Map<RegularPromotionDto>(regular), "Tạo khuyến mãi thường thành công.");
        }

        public async Task<RegularPromotionDto> GetRegularPromotionByIdAsync(int promotionId, CancellationToken ct = default)
        {
            
            var spec = new PromotionByIdWihtTyeSpec(promotionId, (int)PromotionType.Regular);

            var result = await _unitOfWork.Repository<Promotion, int>().FirstOrDefaultProjectedAsync<RegularPromotionDto>(spec, ct);
            if (result == null) throw new NotFoundException("Không tìm thấy khuyến mãi.");

            return result;
        }

        public async Task<FlashSalePromotionDto> GetFlashSalePromotionByIdAsync(int promotionId, CancellationToken ct = default)
        {
            var spec = new PromotionByIdWihtTyeSpec(promotionId, (int)PromotionType.FlashSale);

            var result = await _unitOfWork.Repository<Promotion, int>().FirstOrDefaultProjectedAsync<FlashSalePromotionDto>(spec, ct);
            
            if (result == null) throw new NotFoundException("Không tìm thấy khuyến mãi.");

            return result;
        }

        public async Task<PagedResponse<PromotionDto, PromotionParameters>> GetPromotionsAsync(PromotionParameters parameters, CancellationToken ct = default)
        {

            var spec = new PromotionParametersSpec(parameters);

            var result = await _unitOfWork.Repository<Promotion, int>()
                .GetPagedAsync<PromotionDto, PromotionParameters>(spec ,parameters, ct);

            return result;
        }



        public async Task<PromotionDto?> GetPromotionByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new PromotionByIdSpec(id);

            var result = await _unitOfWork.Repository<Promotion, int>().FirstOrDefaultProjectedAsync<PromotionDto>(spec, ct);

            if (result == null) throw new NotFoundException("Không tìm thấy khuyến mãi.");
            
            return result;
        }
    }
}
