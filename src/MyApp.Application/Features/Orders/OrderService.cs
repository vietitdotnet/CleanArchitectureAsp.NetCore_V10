using AutoMapper;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.Features.Orders.DTOs;
using MyApp.Application.Features.Products.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders
{
    public class OrderService : BaseService, IOderService 
    {
        public OrderService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IServiceProvider serviceProvider) : base(unitOfWork, mapper, serviceProvider)
        {
        }

        public async Task<IReadOnlyList<OrderDto>> GetAllOrdersAsync(CancellationToken ct = default)
        {
            var spec = new OrderSpec();
            var orders = await _unitOfWork.Repository<Order, int>().ListProjectedAsync<OrderDto>(spec, ct);
            return orders;
        }
    }
}
