
using MyApp.Application.Features.Managers.DTOs;
using MyApp.Application.Features.OrderProducts.DTOs;

using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;

namespace MyApp.Application.Features.Orders.DTOs
{
    public class OrderDto : BaseDto
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public UserDto Customer { get; private set; } = null!;

        public decimal TotalPrice { get; private set; }

        public int TotalCount { get; private set; }

        public IReadOnlyList<OrderProductDto> Products { get; private set; } = [];

    }
}
