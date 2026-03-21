using MyApp.Application.Features.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces
{
    public interface IOderService
    {
        public Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync(CancellationToken ct = default);
    }
}
