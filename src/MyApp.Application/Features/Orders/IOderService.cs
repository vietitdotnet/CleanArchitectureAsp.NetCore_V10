using MyApp.Application.Common.Results;
using MyApp.Application.Features.Orders.DTOs;
using MyApp.Application.Features.Orders.Requests;
using MyApp.Domain.Entities;
using MyApp.Domain.Paginations.Core;
using MyApp.Domain.Paginations.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders
{
    public interface IOderService
    {
        public Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync(CancellationToken ct = default);
        public Task<PagedResponse<OrderDto, OrderParameters>> GetOrdersAsync(OrderParameters param, CancellationToken ct = default);
        public Task<OperationResult<OrderDto>> CreateOrderAsync(CreateOrderRequest request, string? userId, CancellationToken ct = default);
    }
}
