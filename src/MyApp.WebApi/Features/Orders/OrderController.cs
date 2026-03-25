using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Orders;
using MyApp.Application.Features.Orders.Responses;
using MyApp.Application.Features.Products.Responses;

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
    }
}
