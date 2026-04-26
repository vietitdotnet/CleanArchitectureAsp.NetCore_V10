using MyApp.Application.Common.Results;
using MyApp.Application.Features.PromotionItems.DTOs;
using MyApp.Application.Features.PromotionItems.Requests;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems
{
    public interface IPromotionItemService
    {
        Task<OperationResult<PromotionItemDto>> UpdatePromotionItemAsync(int id, UpdatePromotionItemRequest request, CancellationToken ct = default);

        Task<PromotionItemDto> GetPromotionItemByIdAsync(int id, CancellationToken ct = default);

        Task<OperationResult<PromotionItemDto>> CreatePromotionItemAsync(int promotionId, CreatePromotionItemRequest request, CancellationToken ct = default);
        
        Task<PagedResponse<PromotionItemDto, PromotionItemParameters>> GetPromotionItemsAsync(PromotionItemParameters param, CancellationToken ct = default);

        Task<PagedResponse<PromotionItemDto, PromotionItemParameters>> GetPromotionItemsByPromotionIdAsync(int promotionId, PromotionItemParameters param, CancellationToken ct = default);


        Task<OperationResult<bool>> SetActiveAsync(int id, bool isActive, CancellationToken ct = default);      

        Task<OperationResult<bool>> DeletePromotionItemAsync(int id, CancellationToken ct = default);

    }
}
