using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Orders;
using MyApp.Domain.Paginations.Parameters;

namespace MyApp.WebMvc.Areas.Manager.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOderService _oderService;
        public OrderController(ILogger<BaseController> logger,
            IOderService oderService) : base(logger)
        {
            _oderService = oderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] OrderParameters param , CancellationToken ct)
        {
            var orders = await _oderService.GetOrdersAsync(param, ct);

            return View(orders);
        }
    }
}
