using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Orders;
using MyApp.Application.Features.Orders.Requests;
using MyApp.Application.Features.Orders.Responses;
using MyApp.Application.Features.Products.Responses;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebApi.Features.Orders
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOderService _orderService;

        public OrderController(IOderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetAllOrdersResponse), StatusCodes.Status200OK)]
        public async Task<GetAllOrdersResponse> GetAllOrders(CancellationToken ct)
        {
            var result = await _orderService.GetAllOrdersAsync(ct);

            return new GetAllOrdersResponse(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOrdersResponse), StatusCodes.Status200OK)]
        public async Task<GetOrdersResponse> GetOrders([FromQuery] OrderParameters param, CancellationToken ct)
        {
            var result = await _orderService.GetOrdersAsync(param);

            return new GetOrdersResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken ct)
        {
            var userId = User?.Identity?.Name ?? "829dd59d-c01f-42d3-b8d8-7f1fb857ef4f"; // Giả sử bạn lấy userId từ Identity

            var result = await _orderService.CreateOrderAsync(request, userId, ct);

            return CreatedAtAction(
                nameof(GetOrders),
                new { id = result.Data.Id },
                result.Data
            );
        }
    }
}
