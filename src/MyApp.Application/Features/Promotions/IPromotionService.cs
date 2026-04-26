using MyApp.Application.Common.Results;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Application.Features.Promotions.DTOs;
using MyApp.Application.Features.Promotions.Requests;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions
{
    public interface IPromotionService
    {

       Task<PromotionDto?> GetPromotionByIdAsync(int id , CancellationToken ct = default);

        Task<PagedResponse<PromotionDto, PromotionParameters>> GetPromotionsAsync(PromotionParameters parameters, CancellationToken ct = default);


        Task<RegularPromotionDto> GetRegularPromotionByIdAsync(int promotionId, CancellationToken ct = default);

        Task<FlashSalePromotionDto> GetFlashSalePromotionByIdAsync(int promotionId, CancellationToken ct = default);

        Task<OperationResult<RegularPromotionDto>> CreateRegularPromotionAsync(CreateRegularPromotionRequest request, CancellationToken ct = default);

        Task<OperationResult<FlashSalePromotionDto>> CreateFlashSalePromotionAsync(CreateFlashSaleRequest request, CancellationToken ct = default);

     
    }
        
}
